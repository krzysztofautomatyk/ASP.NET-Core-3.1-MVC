using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Ksiegarnia.DataAccess.Repository.IRepository;
using Ksiegarnia.Models;
using Ksiegarnia.Utility;
using Microsoft.AspNetCore.Mvc;

namespace Ksiegarnia.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OkladkaController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public OkladkaController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // Zwraca stronę o nazwie Index.cshtml
        public IActionResult Index()
        {
            return View();
        }

        // Zwraca stronę o nazwie Upsert.cshtml
        public IActionResult Upsert(int? id)
        {
            Okladka okladka = new Okladka();
            if(id==null)
            {
                // jeśli brak id to otwórz stronę do tworzenia nowego wpisu
                return View(okladka);
            }

            // jeżli jest id to oznacza, że otwórz stronę do edycji
            //okladka = _unitOfWork.Okladka.Get(id.GetValueOrDefault());

            // STOREDPROCEDURE
            var parameter = new DynamicParameters();
            parameter.Add("@Id", id);
            okladka = _unitOfWork.SP_Call.OneRecord<Okladka>(SD.Proc_Okladka_Get, parameter);

            if (okladka == null)
            {
                // nie znalazłem wpisu w bazie danych
                return NotFound();
            }

            return View(okladka);
        }


        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            //return NotFound();

            // Zwraca wszystkie kategorie w formacie json
            //var allObj = _unitOfWork.Okladka.GetAll();

            // STOREDPROCEDURE
            var allObj = _unitOfWork.SP_Call.List<Okladka>(SD.Proc_Okladka_GetAll, null);
            return Json(new { data = allObj });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Okladka okladka)
        {
            if(ModelState.IsValid)
            {
                // STOREDPROCEDURE
                var parameter = new DynamicParameters();
                parameter.Add(@"Nazwa", okladka.Nazwa);

                if (okladka.Id == 0)
                {
                    //_unitOfWork.Okladka.Add(okladka);
                    // STOREDPROCEDURE
                    _unitOfWork.SP_Call.Execute(SD.Proc_Okladka_Create, parameter);
                }
                else
                {
                    //_unitOfWork.Okladka.Update(okladka);
                    // STOREDPROCEDURE
                    parameter.Add(@"Id", okladka.Id);
                    _unitOfWork.SP_Call.Execute(SD.Proc_Okladka_Update, parameter);
                }
                _unitOfWork.Save();
                //return RedirectToAction("Index");
                return RedirectToAction(nameof(Index));

            }
            return View(okladka);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {         
            //var objFromDb = _unitOfWork.Okladka.Get(id);
            // STOREDPROCEDURE
            var parameter = new DynamicParameters();
            parameter.Add("@Id", id);
            var objFromDb = _unitOfWork.SP_Call.OneRecord<Okladka>(SD.Proc_Okladka_Get, parameter);
            
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Błąd podczas usuwania rekordu..." });
            }

            //_unitOfWork.Okladka.Remove(objFromDb);
            // STOREDPROCEDURE
            _unitOfWork.SP_Call.Execute(SD.Proc_Okladka_Delete, parameter);

            _unitOfWork.Save();
            return Json(new { success = true, message = "Pomyślnie usunięto rekord." });
        }
        #endregion

    }
}
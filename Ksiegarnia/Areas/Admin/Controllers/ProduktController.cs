using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ksiegarnia.DataAccess.Repository.IRepository;
using Ksiegarnia.Models;
using Ksiegarnia.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ksiegarnia.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProduktController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProduktController(IUnitOfWork unitOfWork)
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
            ProduktVM produktVM = new ProduktVM()
            {
                Produkt = new Produkt(),
                ListaKategorii = _unitOfWork.Category.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Nazwa,
                    Value = i.Id.ToString()
                }),
                ListaOkładek = _unitOfWork.Okladka.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Nazwa,
                    Value = i.Id.ToString()
                })
            };

            if (id==null)
            {
                // jeśli brak id to otwórz stronę do tworzenia nowego wpisu
                return View(produktVM);
            }

            // jeżli jest id to oznacza, że otwórz stronę do edycji
            produktVM.Produkt = _unitOfWork.Produkt.Get(id.GetValueOrDefault());
            if(produktVM.Produkt == null)
            {
                // nie znalazłem wpisu w bazie danych
                return NotFound();
            }

            return View(produktVM);
        }


        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            //return NotFound();

            // Zwraca wszystkie kategorie w formacie json
            var allObj = _unitOfWork.Produkt.GetAll(includeProperties: "Kategoria,Okladka");
            //var allObj = _unitOfWork.Produkt.GetAll();
            //var allObj = _unitOfWork.Category.GetAll();
            return Json(new { data = allObj });
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Upsert(Produkt produkt)
        //{
        //    if(ModelState.IsValid)
        //    {
        //        if(produkt.Id == 0)
        //        {
        //            _unitOfWork.Produkt.Add(produkt);
        //        }
        //        else
        //        {
        //            _unitOfWork.Produkt.Update(produkt);
        //        }
        //        _unitOfWork.Save();
        //        //return RedirectToAction("Index");
        //        return RedirectToAction(nameof(Index));

        //    }
        //    return View(produkt);
        //}

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.Produkt.Get(id);
            if(objFromDb == null)
            {
                return Json(new { success = false, message = "Błąd podczas usuwania rekordu..." });
            }
 
            _unitOfWork.Produkt.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Pomyślnie usunięto rekord." });
        }
        #endregion

    }
}
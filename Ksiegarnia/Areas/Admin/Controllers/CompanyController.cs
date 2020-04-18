using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ksiegarnia.DataAccess.Repository.IRepository;
using Ksiegarnia.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ksiegarnia.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CompanyController(IUnitOfWork unitOfWork)
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
            Company company = new Company();
            if(id==null)
            {
                // jeśli brak id to otwórz stronę do tworzenia nowego wpisu
                return View(company);
            }

            // jeżli jest id to oznacza, że otwórz stronę do edycji
            company = _unitOfWork.Company.Get(id.GetValueOrDefault());
            if(company == null)
            {
                // nie znalazłem wpisu w bazie danych
                return NotFound();
            }

            return View(company);
        }


        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            //return NotFound();

            // Zwraca wszystkie kategorie w formacie json
            var allObj = _unitOfWork.Company.GetAll();
            return Json(new { data = allObj });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Company company)
        {
            if(ModelState.IsValid)
            {
                if(company.Id == 0)
                {
                    _unitOfWork.Company.Add(company);
                }
                else
                {
                    _unitOfWork.Company.Update(company);
                }
                _unitOfWork.Save();
                //return RedirectToAction("Index");
                return RedirectToAction(nameof(Index));

            }
            return View(company);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.Company.Get(id);
            if(objFromDb == null)
            {
                return Json(new { success = false, message = "Błąd podczas usuwania rekordu..." });
            }
 
            _unitOfWork.Company.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Pomyślnie usunięto rekord." });
        }
        #endregion

    }
}
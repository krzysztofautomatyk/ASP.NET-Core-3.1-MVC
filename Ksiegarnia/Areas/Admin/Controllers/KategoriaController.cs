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
    public class KategoriaController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public KategoriaController(IUnitOfWork unitOfWork)
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
            Kategoria kategoria = new Kategoria();
            if(id==null)
            {
                // jeśli brak id to otwórz stronę do tworzenia nowego wpisu
                return View(kategoria);
            }

            // jeżli jest id to oznacza, że otwórz stronę do edycji
            kategoria = _unitOfWork.Category.Get(id.GetValueOrDefault());
            if(kategoria == null)
            {
                // nie znalazłem wpisu w bazie danych
                return NotFound();
            }

            return View(kategoria);
        }


        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            //return NotFound();

            // Zwraca wszystkie kategorie w formacie json
            var allObj = _unitOfWork.Category.GetAll();
            return Json(new { data = allObj });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Kategoria kategoria)
        {
            if(ModelState.IsValid)
            {
                if(kategoria.Id == 0)
                {
                    _unitOfWork.Category.Add(kategoria);
                }
                else
                {
                    _unitOfWork.Category.Update(kategoria);
                }
                _unitOfWork.Save();
                //return RedirectToAction("Index");
                return RedirectToAction(nameof(Index));

            }
            return View(kategoria);
        }
        #endregion

    }
}
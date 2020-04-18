using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Ksiegarnia.DataAccess.Repository.IRepository;
using Ksiegarnia.Models;
using Ksiegarnia.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ksiegarnia.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProduktController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;
        public ProduktController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProduktVM produktVM)
        {
            if (ModelState.IsValid)
            {
                string webRootPath = _hostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                if(files.Count > 0)
                {
                    string fileName = Guid.NewGuid().ToString();
                    // W katalogu "wwwroot"
                    var uploads = Path.Combine(webRootPath, @"images\produkty");
                    var extenstion = Path.GetExtension(files[0].FileName);

                    if(produktVM.Produkt.ImageUrl != null)
                    {
                        // To jest dla edycji i musimy wykasować stare obrazy
                        var imagePath = Path.Combine(webRootPath, produktVM.Produkt.ImageUrl.TrimStart('\\'));
                        if(System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }

                    }
                    using (var filesStreams = new FileStream(Path.Combine(uploads, fileName + extenstion), FileMode.Create))
                    {
                        files[0].CopyTo(filesStreams);
                    }
                    produktVM.Produkt.ImageUrl = @"\images\produkty\" + fileName + extenstion;
                }
                else
                {
                    // aktualizacja kiedy nie zmieniamy obrazu
                    if(produktVM.Produkt.Id !=0)
                    {
                        Produkt objFromDb = _unitOfWork.Produkt.Get(produktVM.Produkt.Id);
                        produktVM.Produkt.ImageUrl = objFromDb.ImageUrl;
                    }
                }

                if (produktVM.Produkt.Id == 0)
                {
                    _unitOfWork.Produkt.Add(produktVM.Produkt);
                }
                else
                {
                    _unitOfWork.Produkt.Update(produktVM.Produkt);
                }
                _unitOfWork.Save();
                //return RedirectToAction("Index");
                return RedirectToAction(nameof(Index));

            }
            else
            {
                produktVM.ListaKategorii = _unitOfWork.Category.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Nazwa,
                    Value = i.Id.ToString()
                });
                produktVM.ListaOkładek = _unitOfWork.Okladka.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Nazwa,
                    Value = i.Id.ToString()
                });
                if (produktVM.Produkt.Id !=0 )
                {
                    produktVM.Produkt = _unitOfWork.Produkt.Get(produktVM.Produkt.Id);
                }
            }
            return View(produktVM);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.Produkt.Get(id);
            if(objFromDb == null)
            {
                return Json(new { success = false, message = "Błąd podczas usuwania rekordu..." });
            }

            // To jest dla edycji i musimy wykasować stare obrazy
            string webRootPath = _hostEnvironment.WebRootPath;
            var imagePath = Path.Combine(webRootPath, objFromDb.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }

            _unitOfWork.Produkt.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Pomyślnie usunięto rekord." });
        }
        #endregion

    }
}
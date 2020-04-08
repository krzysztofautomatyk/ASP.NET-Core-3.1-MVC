using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ksiegarnia.DataAccess.Repository.IRepository;
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
        public IActionResult Index()
        {
            return View();
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
        #endregion

    }
}
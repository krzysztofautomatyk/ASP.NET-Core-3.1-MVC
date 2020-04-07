using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Ksiegarnia.Areas.Admin.Controllers
{
    public class KategoriaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
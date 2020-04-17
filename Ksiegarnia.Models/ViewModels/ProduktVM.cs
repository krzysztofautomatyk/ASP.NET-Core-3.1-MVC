using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ksiegarnia.Models.ViewModels
{
    public class ProduktVM
    {
        public Produkt Produkt { get; set; }
        
        // Nuget -> Microsoft.AspNetCore.Mvc
        public IEnumerable<SelectListItem> ListaKategorii { get; set; }

        public IEnumerable<SelectListItem> ListaOkładek { get; set; }
    }
}

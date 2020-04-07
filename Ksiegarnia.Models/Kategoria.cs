using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ksiegarnia.Models
{
    public class Kategoria
    {
        [Key]
        public int Id { get; set; }
        
        [Display(Name="Nazwa Kategorii")]
        [Required]
        [MaxLength(50)]
        public string Nazwa { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ksiegarnia.Models
{
    public class Okladka
    {
        [Key]
        public int Id { get; set; }
        
        [Display(Name="Nazwa okładki")]
        [Required]
        [MaxLength(50)]
        public string Nazwa { get; set; }

    }
}

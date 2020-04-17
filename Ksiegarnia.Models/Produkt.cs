using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ksiegarnia.Models
{
    public class Produkt
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public string ISBN { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        [Range(1, 10000)]
        public double ListPrice { get; set; }
        [Required]
        [Range(1, 10000)]
        public double Price { get; set; }
        [Required]
        [Range(1, 10000)]
        public double Price50 { get; set; }
        [Required]
        [Range(1, 10000)]
        public double Price100 { get; set; }
        public string ImageUrl { get; set; }

        [Required]
        public int KategoriaId { get; set; }

        [ForeignKey("KategoriaId")]
        public Kategoria Kategoria { get; set; }

        [Required]
        public int OkladkaId { get; set; }

        [ForeignKey("OkladkaId")]
        public Okladka Okladka { get; set; }
    }
}

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ksiegarnia.Models
{
    public class ApplicationUser : IdentityUser
    {

        [Required]
        public string Name { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }

        // "?" - zezwala any w bazie to pole mogło być puste
        public int? CompanyId { get; set; }

        [ForeignKey("CompanyId")]
        public Company Company { get; set; }


         //"NotMapped" - oznacza że EntityFramework nie dodada tego pola do bazy danych
        [NotMapped]
        public string Role { get; set; }
    }
}

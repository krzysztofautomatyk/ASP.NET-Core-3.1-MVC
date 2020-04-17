using System;
using System.Collections.Generic;
using System.Text;
using Ksiegarnia.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ksiegarnia.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Dodaje wpis aby Entity wiedział o nowym modelu => Kategoria
        public DbSet<Kategoria> Kategorie { get; set; }

        public DbSet<Okladka> Okladki { get; set; }

        public DbSet<Produkt> Produkty { get; set; }
    }
}

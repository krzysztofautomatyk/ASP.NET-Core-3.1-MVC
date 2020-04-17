using Ksiegarnia.DataAccess.Data;
using Ksiegarnia.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ksiegarnia.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            Okladka = new OkladkaRepository(_db);
            Produkt = new ProduktRepository(_db);
            SP_Call = new SP_Call(_db);
        }

        public IKategoriaRepository Category { get; private set; }

        public IOkladkaRepository Okladka { get; private set; }

        public ISP_Call SP_Call { get; private set; }

        public IProduktRepository Produkt { get; private set; }


        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}

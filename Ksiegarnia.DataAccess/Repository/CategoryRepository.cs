using Ksiegarnia.DataAccess.Data;
using Ksiegarnia.DataAccess.Repository.IRepository;
using Ksiegarnia.Models;
using System.Linq;

namespace Ksiegarnia.DataAccess.Repository
{
    public class CategoryRepository : Repository<Kategoria>, ICategoryRepository
    {
        //private readonly DbContext _db;
        private readonly ApplicationDbContext _db;

        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Kategoria kategoria)
        {
            var objFormDb = _db.Kategorie.FirstOrDefault(s => s.Id == kategoria.Id);
            if(objFormDb != null)
            {
                objFormDb.Nazwa = kategoria.Nazwa;
                //_db.SaveChanges();
            }

        }
    }
}

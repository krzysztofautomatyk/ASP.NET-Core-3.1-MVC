using Ksiegarnia.DataAccess.Data;
using Ksiegarnia.DataAccess.Repository.IRepository;
using Ksiegarnia.Models;
using System.Linq;

namespace Ksiegarnia.DataAccess.Repository
{
    public class ProduktRepository : Repository<Produkt>, IProduktRepository
    {
        //private readonly DbContext _db;
        private readonly ApplicationDbContext _db;

        public ProduktRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Produkt produkt)
        {
            var objFormDb = _db.Produkty.FirstOrDefault(s => s.Id == produkt.Id);
            if(objFormDb != null)
            {
                // image
                if(produkt.ImageUrl != null)
                {
                    objFormDb.ImageUrl = produkt.ImageUrl;
                }
                objFormDb.ISBN = produkt.ISBN;
                objFormDb.Price = produkt.Price;
                objFormDb.Price50 = produkt.Price50;
                objFormDb.ListPrice = produkt.ListPrice;
                objFormDb.Price100 = produkt.Price100;
                objFormDb.Title = produkt.Title;
                objFormDb.Description = produkt.Description;
                objFormDb.KategoriaId = produkt.KategoriaId;
                objFormDb.Author = produkt.Author;
                objFormDb.OkladkaId = produkt.OkladkaId;

            }
            
        }
    }
}

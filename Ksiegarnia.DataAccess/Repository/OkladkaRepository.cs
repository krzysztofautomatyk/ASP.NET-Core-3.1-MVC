using Ksiegarnia.DataAccess.Data;
using Ksiegarnia.DataAccess.Repository.IRepository;
using Ksiegarnia.Models;
using System.Linq;

namespace Ksiegarnia.DataAccess.Repository
{
    public class OkladkaRepository : Repository<Okladka>, IOkladkaRepository
    {
        //private readonly DbContext _db;
        private readonly ApplicationDbContext _db;

        public OkladkaRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Okladka okladka)
        {
            var objFormDb = _db.Okladki.FirstOrDefault(s => s.Id == okladka.Id);
            if(objFormDb != null)
            {
                objFormDb.Nazwa = okladka.Nazwa;
                //_db.SaveChanges();
            }

        }
    }
}

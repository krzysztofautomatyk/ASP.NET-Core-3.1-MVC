using Ksiegarnia.DataAccess.Data;
using Ksiegarnia.DataAccess.Repository.IRepository;
using Ksiegarnia.Models;
using System.Linq;

namespace Ksiegarnia.DataAccess.Repository
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        //private readonly DbContext _db;
        private readonly ApplicationDbContext _db;

        public CompanyRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Company company)
        {
            _db.Update(company);

        }
    }
}

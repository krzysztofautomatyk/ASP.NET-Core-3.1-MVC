using System;
using System.Collections.Generic;
using System.Text;

namespace Ksiegarnia.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IKategoriaRepository Category { get; }

        IOkladkaRepository Okladka { get; }

        IProduktRepository Produkt { get; }

        ISP_Call SP_Call { get; }

        ICompanyRepository Company { get; }
        IApplicationUserRepository applicationUserRepository { get; }

        void Save();
    }
}

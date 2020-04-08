using System;
using System.Collections.Generic;
using System.Text;

namespace Ksiegarnia.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IKategoriaRepository Category { get; }

        IOkladkaRepository Okladka { get; }

        ISP_Call SP_Call { get; }

        void Save();
    }
}

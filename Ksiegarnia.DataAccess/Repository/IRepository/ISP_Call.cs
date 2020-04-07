using Dapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ksiegarnia.DataAccess.Repository.IRepository
{
    // ISP => Interface Stored Procedure

    public interface ISP_Call :IDisposable
    {
        // zwraca INT lub BOOL
        T Single<T>(string procedureName, DynamicParameters param = null);

        void Execute(string procedureName, DynamicParameters param = null);

        // Zwraca cały wiersz
        T OneRecord<T>(string procedureName, DynamicParameters param = null);
    
        IEnumerable<T> List<T>(string procedureName, DynamicParameters param = null);

        // zwraca dwie tabele
        Tuple<IEnumerable<T1>, IEnumerable<T2>> List<T1,T2>(string procedureName, DynamicParameters param = null);

    }
}

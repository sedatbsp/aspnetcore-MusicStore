using Dapper;
using System;
using System.Collections.Generic;

namespace MainMusicStore.DataAccess.IMainRepository
{
    public interface ISPCallRepository : IDisposable
    {
        T Single<T>(string procedurName, DynamicParameters parameters = null);

        void Execute(string procedurName, DynamicParameters parameters = null);

        T OneRecord<T>(string procedurName, DynamicParameters parameters = null);

        IEnumerable<T> List<T>(string procedurName, DynamicParameters parameters = null);

        Tuple<IEnumerable<T1>, IEnumerable<T2>> List<T1, T2>(string procedurName, DynamicParameters parameters = null);



    }
}

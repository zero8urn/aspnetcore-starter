using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace   aspnetcore_starter.DataAccess
{
    public interface IBaseDataAccess
    {
        Task<int> ExecuteAsync(string sql, object param = null, CommandType? commandType = null);
        Task<T> ExecuteScalarAsync<T>(string sql, object param = null);
        Task<List<T>> QueryAsync<T>(string sql, object param = null);
        Task<List<T>> StoredProcedureAsync<T>(string sql, object param = null);
    }
}
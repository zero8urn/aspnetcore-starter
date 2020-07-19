using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace aspnetcore_starter.DataAccess
{
    public class BaseDataAccess : IBaseDataAccess
    {
        private readonly string connectionString;

        public BaseDataAccess(IConfiguration config, string connectionStringName)
        {
            this.connectionString = config.GetConnectionString(connectionStringName);
        }
        public virtual Task<int> ExecuteAsync(string sql, object param = null, CommandType? commandType = null)
        {
            throw new System.NotImplementedException();
        }

        public virtual Task<T> ExecuteScalarAsync<T>(string sql, object param = null)
        {
            throw new System.NotImplementedException();
        }

        public virtual Task<List<T>> QueryAsync<T>(string sql, object param = null)
        {
            throw new System.NotImplementedException();
        }

        public virtual Task<List<T>> StoredProcedureAsync<T>(string sql, object param = null)
        {
            throw new System.NotImplementedException();
        }
    }
}
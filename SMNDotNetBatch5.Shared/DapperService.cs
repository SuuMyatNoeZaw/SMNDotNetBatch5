using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMNDotNetBatch5.Shared
{
    public class DapperService
    {
        private readonly string _connectionString;

        public DapperService(string connectionString)
        {
            _connectionString = connectionString;

        }
        public List<T> Query<T>(string query)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            var list=db.Query<T>(query).ToList();
            return list;
        }
        public T FirstOrDefault<T>(string query, object? param = null)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            var item = db.QueryFirstOrDefault<T>(query,param);
            return item;
        }
    }
}

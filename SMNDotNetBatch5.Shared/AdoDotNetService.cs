
using System.Data;
using System.Data.SqlClient;

namespace SMNDotNetBatch5.Shared
{
    public class AdoDotNetService
    {
        private readonly string _connectionString;

        public AdoDotNetService(string connectionString)
        {
            _connectionString = connectionString;

        }
        public DataTable Query(string query, Parameters[]parameter=null)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand(query, connection);
            if(parameter is not  null)
            {
                foreach (var item in parameter)
                {
                    cmd.Parameters.AddWithValue(item.Name, item.Value);
                }
            }
         
         SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            connection.Close();
            return dt;
        }
    }
    public class Parameters
    {
        public string Name { get; set;}
        public object Value { get; set;}
    }
}
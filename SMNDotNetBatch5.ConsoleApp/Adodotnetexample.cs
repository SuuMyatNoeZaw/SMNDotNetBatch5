using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMNDotNetBatch5.ConsoleApp
{
    public class Adodotnetexample
    {
        string _connectionString = "Data Source=WINDOWS-1ISKG05\\SQLEXPRESS; Initial Catalog=DotNetTrainingBatch5;Trusted_Connection=True;";
        public void Read()
        {

            Console.WriteLine("Hello, World!");

            SqlConnection connection = new SqlConnection(_connectionString);
            Console.WriteLine("Connection Opening.....");
            connection.Open();
            string query = @"SELECT[BlogID]
            ,[BlogTitle]
            ,[BlogAuthor]
            ,[BlogContent]
            ,[DeleteFlag]
        FROM [dbo].[Tbl_Blog]";

            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            Console.WriteLine("Connection Closing.....");
            connection.Close();

            foreach (DataRow dr in dt.Rows)
            {
                Console.WriteLine(dr["BlogID"]);
                Console.WriteLine(dr["BlogTitle"]);
                Console.WriteLine(dr["BlogAuthor"]);
                Console.WriteLine(dr["BlogContent"]);
            }
        }

        public void Create()
        {
            
            Console.WriteLine("Enter Title Name...");
            string title = Console.ReadLine();
            Console.WriteLine("Enter Author Name...");
            string author = Console.ReadLine();
            Console.WriteLine("Enter Content...");
            string content = Console.ReadLine();

            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            string query = $@"INSERT INTO [dbo].[Tbl_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent]
           ,[DeleteFlag])
     VALUES
           (@BlogTitle
           ,@BlogAuthor
           ,@BlogContent
           ,0)";

            SqlCommand cmd = new SqlCommand(query, connection);
            
            cmd.Parameters.AddWithValue("@BlogTitle", title);
            cmd.Parameters.AddWithValue("@BlogAuthor", author);
            cmd.Parameters.AddWithValue("@BlogContent", content);
            //SqlDataAdapter adaptor= new SqlDataAdapter(cmd);
            //DataTable dt = new DataTable();
            //adaptor.Fill(dt);
            int result=cmd.ExecuteNonQuery();
            connection.Close();
            //if(result==1)
            //{
            //    Console.WriteLine("1 row effected.");
            //}

            Console.WriteLine(result == 1 ? "1 row effected" : "Your tast is fill.");
        }

        public void Update()
        {
            Console.WriteLine("Enter ID...");
            string id = Console.ReadLine();
            Console.WriteLine("Enter Title Name...");
            string title = Console.ReadLine();
            Console.WriteLine("Enter Author Name...");
            string author = Console.ReadLine();
            Console.WriteLine("Enter Content...");
            string content = Console.ReadLine();

            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            string query = $@"UPDATE [dbo].[Tbl_Blog]
   SET [BlogTitle] =@BlogTitle
      ,[BlogAuthor] =@BlogAuthor
      ,[BlogContent] =@BLogContent
      ,[DeleteFlag] =0
 WHERE BlogID=@BlogID";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogID", id);
           cmd.Parameters.AddWithValue("@BlogTitle", title);
            cmd.Parameters.AddWithValue("@BlogAuthor", author);
            cmd.Parameters.AddWithValue("@BlogContent", content);

            int result = cmd.ExecuteNonQuery();
            Console.WriteLine(result == 1 ? "1 Row Updated." : "Your task is failed.");

            connection.Close();
           
        }
        public void Delete()
        {

            Console.WriteLine("Enter ID...");
            string id = Console.ReadLine();
            Console.WriteLine("Enter Title Name...");
            string title = Console.ReadLine();
            Console.WriteLine("Enter Author Name...");
            string author = Console.ReadLine();
            Console.WriteLine("Enter Content...");
            string content = Console.ReadLine();

            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            string query = $@"UPDATE [dbo].[Tbl_Blog]
   SET [BlogTitle] =@BlogTitle
      ,[BlogAuthor] =@BlogAuthor
      ,[BlogContent] =@BLogContent
      ,[DeleteFlag] =1
 WHERE BlogID=@BlogID";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogID", id);
            cmd.Parameters.AddWithValue("@BlogTitle", title);
            cmd.Parameters.AddWithValue("@BlogAuthor", author);
            cmd.Parameters.AddWithValue("@BlogContent", content);
           
            int result=cmd.ExecuteNonQuery();
            Console.WriteLine(result==1?"1 Row Deleted.":"Your task is failed.");
        }
    }
}

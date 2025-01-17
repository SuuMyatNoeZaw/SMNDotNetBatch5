using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SMNDotNetBatch5.Database.Models;
using SMNDotNetBatch5.RestAPI.ViewModels;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SMNDotNetBatch5.RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogAdoDotNetController : ControllerBase
    {
        string _connectionString = "Data Source=WINDOWS-1ISKG05\\SQLEXPRESS; Initial Catalog=DotNetTrainingBatch5;Trusted_Connection=True;TrustServerCertificate=True;";
        [HttpGet]
        public IActionResult GetBlogs()
        {
            List<BlogViewModel> list = new List<BlogViewModel>();
            SqlConnection connection = new SqlConnection(_connectionString);

            connection.Open();
            string query = @"SELECT[BlogID]
            ,[BlogTitle]
            ,[BlogAuthor]
            ,[BlogContent]
            ,[DeleteFlag]
        FROM [dbo].[Tbl_Blog] where DeleteFlag=0";

            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine(reader["BlogID"]);
                Console.WriteLine(reader["BlogTitle"]);
                Console.WriteLine(reader["BlogAuthor"]);
                Console.WriteLine(reader["BlogContent"]);
                Console.WriteLine(reader["DeleteFlag"]);
                list.Add(new BlogViewModel
                {
                    Id = Convert.ToInt32(reader["BlogID"]),
                    Title = Convert.ToString(reader["BlogTitle"]),
                    Author = Convert.ToString(reader["BlogAuthor"]),
                    Content = Convert.ToString(reader["BlogContent"]),
                    DeleteFlag = Convert.ToBoolean(reader["DeleteFlag"]),
                });
            }

            connection.Close();
            return Ok(list);
        }

        [HttpPost]
        public IActionResult Create(BlogViewModel blog)
        {
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

            cmd.Parameters.AddWithValue("@BlogTitle", blog.Title);
            cmd.Parameters.AddWithValue("@BlogAuthor", blog.Author);
            cmd.Parameters.AddWithValue("@BlogContent", blog.Content);
            cmd.ExecuteNonQuery();
            connection.Close();
            return Ok();
        }

        [HttpPut("{id}")]
        
        public IActionResult Edit(int id)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            string query = @"SELECT [BlogID]
      ,[BlogTitle]
      ,[BlogAuthor]
      ,[BlogContent]
      ,[DeleteFlag]
  FROM [dbo].[Tbl_Blog] where BlogID=@BlogID";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogID", id);
           SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            connection.Close();
            if(dt.Rows.Count == 0)
            {
                Console.WriteLine("No Data Found.");
            }

            DataRow dr = dt.Rows[0];
            BlogViewModel model = new BlogViewModel();
            model.Id = Convert.ToInt32(dr["BlogID"]);
            model.Title = Convert.ToString(dr["BlogTitle"]);
            model.Author = Convert.ToString(dr["BlogAuthor"]);
            model.Content = Convert.ToString(dr["BlogContent"]);
            model.DeleteFlag = Convert.ToBoolean(dr["DeleteFlag"]);
            return Ok(model);
        }
        [HttpPatch("{id}")]
        public IActionResult Update(int id,BlogViewModel blog) 
        {
            SqlConnection connection=new SqlConnection(_connectionString);
            connection.Open();
            string query = $@"UPDATE [dbo].[Tbl_Blog]
   SET [BlogTitle] =@BlogTitle
      ,[BlogAuthor] =@BlogAuthor
      ,[BlogContent] =@BLogContent
      ,[DeleteFlag] =0
 WHERE BlogID=@BlogID";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogID", id);
            cmd.Parameters.AddWithValue("@BlogTitle", blog.Title);
            cmd.Parameters.AddWithValue("@BlogAuthor", blog.Author);
            cmd.Parameters.AddWithValue("@BlogContent", blog.Content);

            int result = cmd.ExecuteNonQuery();
            Console.WriteLine(result == 1 ? "1 Row Updated." : "Your task is failed.");

            connection.Close();
            return Ok(result);
        }
    }
}

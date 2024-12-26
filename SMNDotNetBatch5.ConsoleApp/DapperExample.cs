using Dapper;
using SMNDotNetBatch5.ConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMNDotNetBatch5.ConsoleApp
{
    public class DapperExample
    {
        string _connectionString = "Data Source=WINDOWS-1ISKG05\\SQLEXPRESS; Initial Catalog=DotNetTrainingBatch5;Trusted_Connection=True;";
        public void Read()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = "select * from Tbl_Blog where DeleteFlag=0";
                var lst = db.Query<BlogDataModel>(query).ToList();
                foreach (var item in lst)
                {
                    Console.WriteLine(item.BlogID);
                    Console.WriteLine(item.BlogTitle);
                    Console.WriteLine(item.BlogAuthor);
                    Console.WriteLine(item.BlogContent);
                    Console.WriteLine(item.DeleteFlag);
                }
            }


        }
        public void Creat(string title, string author, string content)
        {
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
            using (IDbConnection db = new SqlConnection(_connectionString))
            {

                int result = db.Execute(query, new
                {
                    BlogTitle = title,
                    BlogAuthor = author,
                    BlogContent = content,
                });
                Console.WriteLine(result == 1 ? "1 row effected." : "Your task is failed.");
            }

        }
        public void Update(int id, string title, string author, string content)
        {
            string query = $@"UPDATE [dbo].[Tbl_Blog]
   SET [BlogTitle] =@BlogTitle
      ,[BlogAuthor] =@BlogAuthor
      ,[BlogContent] =@BLogContent
      ,[DeleteFlag] =0
 WHERE BlogID=@BlogID";
            using (IDbConnection db = new SqlConnection(_connectionString))
            {

                int result = db.Execute(query, new
                {
                    BlogID = id,
                    BlogTitle = title,
                    BlogAuthor = author,
                    BlogContent = content,
                }) ;
                Console.WriteLine(result == 1 ? "1 row effected." : "Your task is failed.");
            }
        }
        public void Delete(int id,string title,string author,string content)
        {
            string query = $@"UPDATE [dbo].[Tbl_Blog]
   SET [BlogTitle] =@BlogTitle
      ,[BlogAuthor] =@BlogAuthor
      ,[BlogContent] =@BLogContent
      ,[DeleteFlag] =1
 WHERE BlogID=@BlogID";
            using(IDbConnection db=new SqlConnection(_connectionString))
            {
                int result = db.Execute(query, new 
                { 
                BlogID=id,
                BlogTitle=title,
                BlogAuthor=author,
                BlogContent=content,
                });
                Console.WriteLine(result == 1 ? "1 row effected." : "Your task is failed");
            }
        }
    }
}

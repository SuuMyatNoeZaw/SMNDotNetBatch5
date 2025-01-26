using SMNDotNetBatch5.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMNDotNetBatch5.ConsoleApp
{
    internal class AdoDotNetShareExample
    {
      string  _connectionString = "Data Source=WINDOWS-1ISKG05\\SQLEXPRESS; Initial Catalog=DotNetTrainingBatch5;Trusted_Connection=True;";
        private readonly AdoDotNetService _adoDotNetService;
        public AdoDotNetShareExample()
        {
            _adoDotNetService = new AdoDotNetService(_connectionString);
        }
        public void Read()
        {
            string query = @"SELECT[BlogID]
            ,[BlogTitle]
            ,[BlogAuthor]
            ,[BlogContent]
            ,[DeleteFlag]
        FROM [dbo].[Tbl_Blog]";
           var dt= _adoDotNetService.Query(query);
            foreach (DataRow dr in dt.Rows)
            {
                Console.WriteLine(dr["BlogID"]);
                Console.WriteLine(dr["BlogTitle"]);
                Console.WriteLine(dr["BlogAuthor"]);
                Console.WriteLine(dr["BlogContent"]);
            }
        }


        public void Edit()
        {
            Console.WriteLine("Enter Id");
           string id= Console.ReadLine();
            string query = $@"SELECT[BlogID]
            ,[BlogTitle]
            ,[BlogAuthor]
            ,[BlogContent]
            ,[DeleteFlag]
        FROM [dbo].[Tbl_Blog] WHERE BlogID=@BlogID";
            var dt = _adoDotNetService.Query(query, new Parameters
            {
                Name = "@BlogID",
                Value=id
            }) ;
            DataRow dr = dt.Rows[0];
            Console.WriteLine(dr["BlogID"]);
            Console.WriteLine(dr["BlogTitle"]);
            Console.WriteLine(dr["BlogAuthor"]);
            Console.WriteLine(dr["BlogContent"]);
        }
        public void Create()
        {

            Console.WriteLine("Enter Title Name...");
            string title = Console.ReadLine();
            Console.WriteLine("Enter Author Name...");
            string author = Console.ReadLine();
            Console.WriteLine("Enter Content...");
            string content = Console.ReadLine();

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

            int result = _adoDotNetService.Execute(query, 
            new Parameters
            {
                Name = "@BlogTitle",
                Value = title
            },
            new Parameters
            {
                Name = "@BlogAuthor",
                Value = author
            },
            new Parameters
            {
                Name = "@BlogContent",
                Value = content
            }
             );

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

           
            string query = $@"UPDATE [dbo].[Tbl_Blog]
   SET [BlogTitle] =@BlogTitle
      ,[BlogAuthor] =@BlogAuthor
      ,[BlogContent] =@BLogContent
      ,[DeleteFlag] =0
 WHERE BlogID=@BlogID";

            int result = _adoDotNetService.Execute(query, new Parameters
            {
                Name="@BlogID",
                Value=id
            },
            new Parameters
            {
                Name = "@BlogTitle",
                Value = title
            },
            new Parameters
            {
                Name = "@BlogAuthor",
                Value = author
            },
            new Parameters
            {
                Name = "@BlogContent",
                Value = content
            }
             );
            

            
            Console.WriteLine(result == 1 ? "1 Row Updated." : "Your task is failed.");

            

        }
    }
}

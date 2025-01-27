using SMNDotNetBatch5.ConsoleApp.Models;
using SMNDotNetBatch5.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMNDotNetBatch5.ConsoleApp
{
    public class DapperShareExample
    {
        string _connectionString = "Data Source=WINDOWS-1ISKG05\\SQLEXPRESS; Initial Catalog=DotNetTrainingBatch5;Trusted_Connection=True;";
        private readonly DapperService _dapperService;
        public DapperShareExample()
        {
            _dapperService = new DapperService(_connectionString);
        }
        public void Read()
        {

            string query = "select * from Tbl_Blog where DeleteFlag=0";
            var lst = _dapperService.Query<BlogDapperDataModel>(query).ToList();
            foreach (var item in lst)
            {
                Console.WriteLine(item.BlogID);
                Console.WriteLine(item.BlogTitle);
                Console.WriteLine(item.BlogAuthor);
                Console.WriteLine(item.BlogContent);
                Console.WriteLine(item.DeleteFlag);
            }
        }
        public void Edit(int id)
        {

            string query = "select * from Tbl_Blog where DeleteFlag=0 and BlogID=@BlogID";
            var item = _dapperService.FirstOrDefault<BlogDapperDataModel>(query,new BlogDapperDataModel
            {
                BlogID=id
            });
            if(item is null)
            {
                Console.WriteLine("No Data Found.");
                return;
            }
                Console.WriteLine(item.BlogID);
                Console.WriteLine(item.BlogTitle);
                Console.WriteLine(item.BlogAuthor);
                Console.WriteLine(item.BlogContent);
                Console.WriteLine(item.DeleteFlag);
            
        }
    }
    
}

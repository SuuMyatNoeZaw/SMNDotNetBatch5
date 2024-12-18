// See https://aka.ms/new-console-template for more information
using System;
using System.Data;
using System.Data.SqlClient;

Console.WriteLine("Hello, World!");
string connectionString = "Data Source=WINDOWS-1ISKG05\\SQLEXPRESS; Initial Catalog=DotNetTrainingBatch5;Trusted_Connection=True;";
SqlConnection connection = new SqlConnection(connectionString);
Console.WriteLine("Connection Opening.....");
connection.Open();
string query = @"SELECT[BlogID]
      ,[BlogTitle]
      ,[BlogAuthor]
      ,[BlogContent]
      ,[DeleteFlag]
  FROM [dbo].[Tbl_Blog]";

    SqlCommand cmd =new SqlCommand(query, connection);
SqlDataAdapter adapter = new SqlDataAdapter(cmd);
DataTable dt=new DataTable();
adapter.Fill(dt);
Console.WriteLine("Connection Closing.....");
connection.Close();

foreach(DataRow dr in dt.Rows)
{
    Console.WriteLine(dr["BlogID"]);
    Console.WriteLine(dr["BlogTitle"]);
    Console.WriteLine(dr["BlogAuthor"]);
    Console.WriteLine(dr["BlogContent"]);
}

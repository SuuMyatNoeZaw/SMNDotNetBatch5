// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;
using SMNDotNetBatch5.Database.Models;

//Console.WriteLine("Hello, World!");
//AppDbContext db=new AppDbContext();
//var list=db.TblBlogs.ToList();
var ones = new Jsmodel
{
    ID = 1,
    Name = "Khine Khine",
    Job = "Teacher",

};
string jsonStr=JsonConvert.SerializeObject(ones,Formatting.Indented);
Console.WriteLine(jsonStr);
public class Jsmodel
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Job { get; set; }
}
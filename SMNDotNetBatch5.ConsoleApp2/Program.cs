// See https://aka.ms/new-console-template for more information
using SMNDotNetBatch5.Database.Models;

Console.WriteLine("Hello, World!");
AppDbContext db=new AppDbContext();
var list=db.TblBlogs.ToList();
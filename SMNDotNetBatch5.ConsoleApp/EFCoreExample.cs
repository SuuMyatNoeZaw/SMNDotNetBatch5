using Microsoft.EntityFrameworkCore;
using SMNDotNetBatch5.ConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMNDotNetBatch5.ConsoleApp
{
    public class EFCoreExample
    {
        public void Read()
        {
            AppDbContext db = new AppDbContext();
            var list=db.Blogs.ToList();
            foreach (var item in list)
            {
                Console.WriteLine(item.BlogID);
                Console.WriteLine(item.BlogTitle);
                Console.WriteLine(item.BlogAuthor);
                Console.WriteLine(item.BlogContent);
                Console.WriteLine(item.DeleteFlag);
            }
        }
        public void Create(string title,string author,string content)
        {
            BlogDataModel blog = new BlogDataModel
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };
            AppDbContext db=new AppDbContext();
            db.Blogs.Add(blog);
            int result=db.SaveChanges();
            Console.WriteLine(result==1?"1 rows effected.":"Your task is failed.");
        }

        public void Edit(int id)
        {
            AppDbContext db = new AppDbContext();
            var item=db.Blogs.Where(x=>x.BlogID==id).FirstOrDefault();
            if(item is null)
            {
                Console.WriteLine("No data found.");
                return;
            }
            Console.WriteLine(item.BlogID);
            Console.WriteLine(item.BlogTitle);
            Console.WriteLine(item.BlogAuthor);
            Console.WriteLine(item.BlogContent);
        }

        public void Update(int id,string title,string author,string content)
        {
            AppDbContext db = new AppDbContext();
            var item = db.Blogs.AsNoTracking().FirstOrDefault(x => x.BlogID == id);
            if (item is null)
            {
                Console.WriteLine("No data found.");
                return;
            }
            if(!string.IsNullOrEmpty(title))
            {
                item.BlogTitle = title;
            }
            if (!string.IsNullOrEmpty(author))
            {
                item.BlogAuthor = author;
            }
            if (!string.IsNullOrEmpty(content))
            {
                item.BlogContent = content;
            }
            db.Entry(item).State = EntityState.Modified;
           int result= db.SaveChanges();
            
            Console.WriteLine(result==1?"1 row effected.":"Your task is failed.");
        }

        public void Delete(int id)
        {
            AppDbContext db = new AppDbContext();
            var item = db.Blogs.AsNoTracking().FirstOrDefault(x => x.BlogID == id);
            if (item is null)
            {
                Console.WriteLine("No data found.");
                return;
            }
            db.Entry(item).State = EntityState.Deleted;
            int result=db.SaveChanges();
            Console.WriteLine(result == 1 ? "1 row effected." : "Your task is failed.");
        }
    }
}

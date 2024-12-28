﻿using SMNDotNetBatch5.ConsoleApp.Models;
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
    }
}

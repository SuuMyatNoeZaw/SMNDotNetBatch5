using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update;
using SMNDotNetBatch5.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMNDotNetBatch5.Domain.Feature.Blog
{
    public class BlogService
    {
        private readonly AppDbContext _db = new AppDbContext();
        public List<TblBlog> GetBlogs()
        {
            var model = _db.TblBlogs.AsNoTracking().ToList();
            return model;
        }
        public TblBlog Create(TblBlog blog)
        {
            _db.TblBlogs.Add(blog);
            _db.SaveChanges();
            return blog;
        }
        public TblBlog GetID(int id)
        {
            var item = _db.TblBlogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);

            return item;
        }
        public TblBlog Update(int id, TblBlog blog)
        {
            var item = _db.TblBlogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
            if (item != null)
            {
                return null;
            }
            item.BlogTitle = blog.BlogTitle;
            item.BlogAuthor = blog.BlogAuthor;
            item.BlogContent = blog.BlogContent;
            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
            return item;
        }
        public TblBlog UpdateByID(int id, TblBlog blog)
        {
            var item = _db.TblBlogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
            if (item != null)
            {
                return null;
            }
            if(!String.IsNullOrEmpty(blog.BlogTitle))
            {
                item.BlogTitle = blog.BlogTitle;
            }
           if(!String.IsNullOrEmpty(blog.BlogAuthor))
            {
                item.BlogAuthor = blog.BlogAuthor;
            }
           if(!String.IsNullOrEmpty(blog.BlogContent))
            {
                item.BlogContent = blog.BlogContent;
            }
            
            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
            return item;
        }
        public bool? Delete(int id)
        {
            var item = _db.TblBlogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
            if (item != null)
            {
                return false;
            }
            _db.Entry(item).State = EntityState.Deleted;
            int result = _db.SaveChanges();
            return result > 0;
        }
    }
}

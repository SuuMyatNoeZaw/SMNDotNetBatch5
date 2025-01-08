using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SMNDotNetBatch5.Database.Models;

namespace SMNDotNetBatch5.RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly AppDbContext _db = new AppDbContext();
        [HttpGet]
        public IActionResult GetBlogs()
        {
            var list = _db.TblBlogs.AsNoTracking().ToList();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public IActionResult CreateBlogs(int id)
        {
            var item = _db.TblBlogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                return NotFound();
            }
            return Ok(item);
        }


        [HttpPost]
        public IActionResult UpdateBlogs(TblBlog blog) 
        {
            _db.TblBlogs.Add(blog);
            _db.SaveChanges();
            return Ok(blog);
        }

        [HttpPut("{id}")]
        public IActionResult CreateBlogs(int id,TblBlog blog)
        {
            var item = _db.TblBlogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                return NotFound();
            }
            item.BlogTitle = blog.BlogTitle;
            item.BlogAuthor = blog.BlogAuthor;
            item.BlogContent = blog.BlogContent;
            _db.Entry(item).State= EntityState.Modified;
            _db.SaveChanges();
            return Ok();
        }

        

        [HttpPatch("{id}")]
        public IActionResult PatchBlogs(int id,TblBlog blog)
        {
            var item = _db.TblBlogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                return NotFound();
            }
            if(!string.IsNullOrEmpty(blog.BlogTitle))
            {
                item.BlogTitle = blog.BlogTitle;
            }
            if (!string.IsNullOrEmpty(blog.BlogAuthor))
            {
                item.BlogAuthor = blog.BlogAuthor;
            }
            if (!string.IsNullOrEmpty(blog.BlogContent))
            {
                item.BlogContent = blog.BlogContent;
            }
            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
            return Ok(item);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlogs(int id)
        {
            var item = _db.TblBlogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                return NotFound();
            }
            _db.Entry(item).State = EntityState.Deleted;
            _db.SaveChanges();
            return Ok();
        }
    }
}

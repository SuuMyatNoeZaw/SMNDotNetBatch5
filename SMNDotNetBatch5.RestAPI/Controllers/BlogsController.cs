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
      private readonly  AppDbContext _db=new AppDbContext();
        [HttpGet]
        public IActionResult GetBlogs()
        {
            var list = _db.TblBlogs.AsNoTracking().ToList();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public IActionResult CreateBlogs(int id)
        {
            var item=_db.TblBlogs.AsNoTracking().FirstOrDefault(x=>x.BlogId == id);
            if(item is null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPost]
        public IActionResult CreateBlogs()
        {
            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateBlogs()
        {
            return Ok();
        }

        [HttpPatch]
        public IActionResult PatchBlogs()
        {
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteBlogs()
        {
            return Ok();
        }
    }
}

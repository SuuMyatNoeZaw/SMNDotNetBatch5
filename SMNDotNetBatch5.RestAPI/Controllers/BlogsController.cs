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

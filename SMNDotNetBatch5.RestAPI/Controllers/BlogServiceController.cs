using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SMNDotNetBatch5.Database.Models;
using SMNDotNetBatch5.Domain.Feature.Blog;

namespace SMNDotNetBatch5.RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogServiceController : ControllerBase
    {
        private readonly BlogService _service;
        public BlogServiceController()
        {
            _service = new BlogService();
        }
        [HttpGet]
        public IActionResult GetBlogs()
        {
            var list = _service.GetBlogs();
            return Ok(list);
        }
        [HttpGet("{id}")]
        public IActionResult GetBlogs(int id)
        {
            var item = _service.GetID(id);
            return Ok(item);
        }
        [HttpPost]
        public IActionResult CreateBlog(TblBlog blog)
        {
            var item = _service.Create(blog);
            return Ok(item);
        }
        [HttpPut]
        public IActionResult UpdateBlog(int id,TblBlog blog)
        {
            var item = _service.UpdateByID(id,blog);
            return Ok(item);
        }
        [HttpPatch]
        public IActionResult UpdatePatch(int id, TblBlog blog)
        {
            var item = _service.UpdateByID(id, blog);
            return Ok(item);
        }
        [HttpDelete]
        public IActionResult DeleteBlog(int id)
        {
          var item = _service.Delete(id);
            if(item is null)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}

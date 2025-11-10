using Blog.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BloggerController : ControllerBase
    {
        [HttpPost()]
        public ActionResult AddNewBLogger(Blogger blogger)
        {
            using (var db = new BlogDBContext())
            {
                blogger.RegTime = DateTime.Now;
                db.Bloggers.Add(blogger);
                db.SaveChanges();
            }
            return Ok();

        }

    }
}

using Blog.Models;
using Blog.Models.Dtios;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BloggerController : ControllerBase
    {
        [HttpPost()]
        public ActionResult AddNewBLogger(AddBloggerDto blogger)
        {

            try
            {
                var newBlogger = new BloggerDBContext
                {
                    Name = blogger.Name,
                    Email = blogger.Email,
                    Password = blogger.Password
                };
                using (var context = new BlogDBContext())
                {
                    if (newBlogger != null)
                    {
                        context.Bloggers.Add(newBlogger);
                        context.SaveChanges();
                        return StatusCode(201, new {message = "Sikeres hozzáadás.", result = newBlogger});
                    }
                    return NotFound(new { message = "Nincs blogger", result = newBlogger});
                }
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = ex.Message, result ="" });
            }

        }

    }
}

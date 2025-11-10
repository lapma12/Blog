using Blog.Models;
using Blog.Models.Dtios;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;

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
                        return StatusCode(201, new { message = "Sikeres hozzáadás.", result = newBlogger });
                    }
                    return NotFound(new { message = "Nincs blogger", result = newBlogger });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = ex.Message, result = "" });
            }

        }

        [HttpGet]
        public ActionResult GetAllBlogger()
        {
            try
            {
                using (var context = new BlogDBContext())
                {
                    return Ok(new { message = "Sikeres lekérdezés.", result = context.Bloggers.ToList() });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = ex.Message, result = "" });

            }
        }

        [HttpGet("GetById")]
        public ActionResult GetBloggerById(int id)
        {
            try
            {
                using (var context = new BlogDBContext())
                {
                    var blog = context.Bloggers.FirstOrDefault(blogger => blogger.Id == id);
                    if (blog != null)
                    {
                        return Ok(new { message = "Sikeres lekérdezés.", result = blog });
                    }
                    return NotFound(new { message = "Nincs ilyen blogger", result = "" });
                }
            }
            catch (Exception)
            {

                return BadRequest(new { message = "Hiba történt", result = "" });
            }    
        }
        [HttpDelete]
        public ActionResult DeleteBlogger(int id)
        {
            try
            {
                using (var context = new BlogDBContext())
                {
                    var blogger = context.Bloggers.FirstOrDefault(b => b.Id == id);

                    if(blogger != null)
                    {
                        context.Bloggers.Remove(blogger);
                        context.SaveChanges();
                        return Ok(new { message = "Sikeres Törlés.", result = blogger });
                    }
                    return NotFound(new { message = "Nincs ilyen blogger", result = "" });
                }
            }
            catch (Exception)
            {

                return BadRequest(new { message = "Hiba történt", result = "" });
            }
        }
    }
}

using Blog.Models;
using Blog.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI.Common;

namespace Blog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        [HttpPost]
        public IActionResult ADdNewPost(AddPostDto addPostDto)
        {
            try
            {
                var post = new Posts
                {
                    Category = addPostDto.Category,
                    Post = addPostDto.Post,
                    PostDate = DateTime.Now,
                    ModTime = DateTime.Now,
                    BloggerId = addPostDto.BloggerId
                };
                using (var context = new BlogDBContext())
                {
                    if (post != null)
                    {
                        context.Posts.Add(post);
                        context.SaveChanges();
                        return StatusCode(201, new { message = "Sikeres hozzáadás.", result = post });
                    }
                    return NotFound(new { message = "Nincs post", result = post });
                }
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Hiba történt", result = "" });
            }
        }
        [HttpGet]
        public IActionResult GetAllPost()
        {
            try
            {
                using (var context = new BlogDBContext())
                {
                    return Ok(new { message = "Sikeres lekérdezés.", result = context.Posts.ToList() });
                }
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Hiba történt", result = "" });
            }
        }

        [HttpGet("ById")]
        public IActionResult GetRecordByID(int Id)
        {
            try
            {
                using (var context = new BlogDBContext())
                {
                    var blogPost = context.Posts.FirstOrDefault(b => b.Azon == Id);
                    if (blogPost != null)
                    {
                        return Ok(new { message = "Sikeres lekérdezés.", result = blogPost });
                    }
                    return NotFound(new { message = "Nincs ilyen post", result = "" });
                }
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Hiba történt", result = "" });
            }
        }

        [HttpDelete]
        public IActionResult DeleteRecord(int Id)
        {
            try
            {
                using (var context = new BlogDBContext())
                {
                    var blogPost = context.Posts.FirstOrDefault(b => b.Azon == Id);
                    if (blogPost != null)
                    {
                        context.Posts.Remove(blogPost);
                        context.SaveChanges();
                        return Ok(new { message = "Sikeres törlés.", result = blogPost });
                    }
                    return NotFound(new { message = "Nincs ilyen post", result = "" });
                }
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Hiba történt", result = "" });
            }
        }
        [HttpPut]
        public IActionResult UpdateRecord(int Id,UpdatePostDto posts)
        {
            try
            {
                using (var context = new BlogDBContext())
                {
                    var existingPost = context.Posts.FirstOrDefault(b => b.Azon == Id);
                    if (existingPost != null)
                    {
                        existingPost.Category = posts.Category;
                        existingPost.Post = posts.Post;
                        existingPost.ModTime = DateTime.Now;
                        context.SaveChanges();
                        return Ok(new { message = "Sikeres módosítás.", result = existingPost });
                    }
                    return NotFound(new { message = "Nincs ilyen post.", result = "" });
                }
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Hiba történt", result = "" });
            }
        }
    }
}

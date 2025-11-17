using Blog.Models;
using Blog.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public IActionResult UpdateRecord(int Id, UpdatePostDto posts)
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
        //Összetett lekérdedések
        [HttpGet("withPosts")]
        public IActionResult GetBloggerWithPosts()
        {
            try
            {
                using (var context = new BlogDBContext())
                {
                    var bloggersWithPosts = context.Bloggers.Include(b => b.Posts).ToList();
                    return Ok(new { message = "Sikeres lekérdezés.", result = bloggersWithPosts });
                }
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Hiba történt", result = "" });
            }
        }
        [HttpGet("getByIdWithPots")]
        public IActionResult GetBloggerByIdWithPosts(int id)
        {
            try
            {
                using (var context = new BlogDBContext())
                {
                    var bloggerWithPosts = context.Bloggers.Include(b => b.Posts).FirstOrDefault(b => b.Id == id);

                    if (bloggerWithPosts != null)
                    {
                        return Ok(new { message = "Sikeres lekérdezés.", result = bloggerWithPosts });
                    }
                    return NotFound(new { message = "Nincs ilyen blogger.", result = "" });
                }
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Hiba történt", result = "" });
            }

        }
        [HttpGet("Name&Category")]
        public IActionResult GetNameCategory(int id)
        {
            try
            {
                using (var context = new BlogDBContext())
                {
                    var bloggernameCategory = context.Bloggers
                        .Where(b => b.Id == id)
                        .SelectMany(b => b.Posts, (b, p) => new
                        {
                            BloggerName = b.Name,
                            PostCategory = p.Category
                        });
                    if (bloggernameCategory != null)
                    {
                        return Ok(new { message = "Sikeres lekérdezés.", result = bloggernameCategory.ToList() });
                    }
                    return NotFound(new { message = "Nincs ilyen adat.", result = "" });
                }
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Hiba történt", result = "" });
            }
        }
    }
}

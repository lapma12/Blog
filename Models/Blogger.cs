using System.ComponentModel.DataAnnotations;

namespace Blog.Models
{
    public class BloggerDBContext
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Email { get; set; }
        
        public string? Phone { get; set; }
        public string? Password { get; set; }
        public DateTime RegTime { get; set; } = DateTime.Now;
        public DateTime ModDate { get; set; } = DateTime.Now;


    }
}

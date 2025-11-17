using System.ComponentModel.DataAnnotations;

namespace Blog.Models
{
    public class Blogger
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string? Password { get; set; }
        public DateTime RegTime { get; set; } = DateTime.Now;
        public DateTime ModDate { get; set; } = DateTime.Now;

        public virtual ICollection<Posts> Posts { get; set; } = new List<Posts>();
    }
}

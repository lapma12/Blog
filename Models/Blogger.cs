namespace Blog.Models
{
    public class BloggerDBContext
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        
        public string Password { get; set; }
        public DateTime RegTime { get; set; } = DateTime.Now;


    }
}

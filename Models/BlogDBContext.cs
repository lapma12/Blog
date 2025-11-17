using Microsoft.EntityFrameworkCore;

namespace Blog.Models
{
    public class BlogDBContext : DbContext
    {
        public BlogDBContext() { }

        public BlogDBContext(DbContextOptions options) : base(options) {}

        public DbSet<Blogger> Bloggers { get; set; }

        public DbSet<Posts> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=blog;user=root;password=;sslmode=Disabled");
        }
    }
}

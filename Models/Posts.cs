using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Models
{
    public class Posts
    {
        [Key]
        public int Azon { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string? Category { get; set; }
        [Column("text")]
        public string Post { get; set; }

        public DateTime PostDate { get; set; } = DateTime.Now;

        public DateTime ModTime { get; set; } = DateTime.Now;

        public int BloggerId { get; set; }

        public virtual Blogger Blogger { get; set; }
    }
}

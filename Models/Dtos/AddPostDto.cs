namespace Blog.Models.Dtos
{
    public class AddPostDto
    {
        public string? Category { get; set; }
        public string Post { get; set; }
        public int BloggerId { get; set; }
    }
}

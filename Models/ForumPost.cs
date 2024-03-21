using System.ComponentModel.DataAnnotations;

namespace ForumProject.Models
{
    public class ForumPost
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public string? Author { get; set; }
        [Required]
        public String DateCreated { get; set; }
        public ICollection<PostComment>? Comments { get; set; }
    }
}

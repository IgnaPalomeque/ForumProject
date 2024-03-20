using System.ComponentModel.DataAnnotations;

namespace ForumProject.Models
{
    public class ForumPost
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public string Creator { get; set; }
        public DateTime TimeCreated{ get; set; } = DateTime.Now;
    }
}

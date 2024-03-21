using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ForumProject.Models
{
    public class PostComment
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string? Body { get; set; }
        [Required]
        public int PostID { get; set; }
        [ForeignKey("PostID")]
        public ForumPost? ForumPost { get; set; }
    }
}

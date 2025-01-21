using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyInstagram.Models
{
    public class Comment 
    {
        [Key]
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        [ForeignKey("Post")]
        public int PostId { get; set; }
        public Post Post { get; set; } = default!;
        public string UserId { get; set; }
    }
}

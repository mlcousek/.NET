using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyInstagram.Models
{
    public class Post 
    {
        [Key]
        public int Id { get; set; }
        public string Text { get; set; }
        public string ImgPath { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;

        [NotMapped]
        public List<int>? Comments { get; set; } = new List<int>();

        [Column("Comments")]
        public string? CommentsSerialized
        {
            get => Comments.Count == 0 ? null : JsonConvert.SerializeObject(Comments); 
            set => Comments = string.IsNullOrEmpty(value) ? new List<int>() : JsonConvert.DeserializeObject<List<int>>(value);
        }

        [NotMapped]
        public List<string>? Likes { get; set; }

        [Column("Likes")]
        public string? LikesSerialized
        {
            get => Likes.Count == 0 ? null : JsonConvert.SerializeObject(Likes);
            set => Likes = string.IsNullOrEmpty(value) ? new List<string>() : JsonConvert.DeserializeObject<List<string>>(value);
        }

        [ForeignKey("User")] 
        public string UserId { get; set; }

        public User User { get; set; } = default!;

        //ZAKLAD PAK DALSI ATRIBUTY
    }
}

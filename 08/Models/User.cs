using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyInstagram.Models
{
    public class User : IdentityUser
    {
        public string? Jmeno { get; set; }
        public string? Prijmeni { get; set; }
        public string? UzivatelskeJmeno { get; set; }
        public int? Vek { get; set; }
        public int? Pohlavi { get; set; }
        public ICollection<Post>? Posts { get; set; } = new List<Post>();

        [NotMapped]
        public List<string>? Comments { get; set; } = new List<string>();

        [Column("Comments")]
        public string? CommentsSerialized
        {
            get => Comments.Count == 0 ? null : JsonConvert.SerializeObject(Comments); 
            set => Comments = string.IsNullOrEmpty(value) ? new List<string>() : JsonConvert.DeserializeObject<List<string>>(value);
        }

        [NotMapped]
        public List<string>? Following { get; set; } = new List<string>();

        [Column("Following")]
        public string? FollowingSerialized
        {
            get => Following.Count == 0 ? null : JsonConvert.SerializeObject(Following); 
            set => Following = string.IsNullOrEmpty(value) ? new List<string>() : JsonConvert.DeserializeObject<List<string>>(value);
        }

        [NotMapped]
        public List<string>? Followed { get; set; } = new List<string>();

        [Column("Followed")]
        public string? FollowedSerialized
        {
            get => Followed.Count == 0 ? null : JsonConvert.SerializeObject(Followed);
            set => Followed = string.IsNullOrEmpty(value) ? new List<string>() : JsonConvert.DeserializeObject<List<string>>(value);
        }


        //ZAKLAD PAK DALSI ATRIBUTY
    }
}

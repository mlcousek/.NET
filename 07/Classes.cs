using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNE07
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

        public virtual ICollection<User> Following { get; set; } = new List<User>(); 
        public virtual ICollection<User> Followed { get; set; } = new List<User>(); 
    }

    public class Post
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string ImgPath { get; set; }
        public List<string> Tags { get; set; } = new List<string>();
        public DateTime ReleaseDate { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>(); 
        public virtual ICollection<User> Likes { get; set; } = new List<User>(); 
    }

    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public int PostId { get; set; }
        public virtual Post Post { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<User> Likes { get; set; } = new List<User>(); 
    }

    public class Message
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime SentDate { get; set; }

        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public virtual User Sender { get; set; } 
        public virtual User Receiver { get; set; } 
    }
}

using Blog.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
        public Post Post { get; set; }
        public int PostId { get; set; }
        public Guid AuthorId { get; set; }
        public User Author { get; set; }
    }

    public class CommentDTO
    {
        public string Content { get; set; }
        public int PostId { get; set; }
    }
}
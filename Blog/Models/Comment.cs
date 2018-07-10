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
        public string AuthorName { get; set; }
    }

    public class CommentDTO
    {
        public string Content { get; set; }
        public int PostId { get; set; }
    }
}
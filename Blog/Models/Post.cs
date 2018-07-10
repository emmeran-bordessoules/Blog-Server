using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public string AuthorName { get; set; }
    }

    public class PostDTO
    {
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
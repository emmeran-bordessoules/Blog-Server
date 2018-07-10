using Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Blog.Controllers
{
    [RoutePrefix("api/posts/{postId}/comments")]
    public class PostCommentsController : ApiController
    {
        private BlogContext db = new BlogContext();

        // GET: api/PostComments
        [Route]
        public IQueryable<Comment> Get(int postId)
        {
            var comments = from c in db.Comments
                           where c.PostId == postId
                           select c;

            return comments;
        }
    }
}

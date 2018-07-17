using Blog.DAL;
using Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace Blog.Controllers
{
    [RoutePrefix("api/posts/{postId}/comments")]
    public class PostCommentsController : ApiController
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        // GET: api/PostComments
        [Route]
        public IEnumerable<CommentDTO> Get(int postId)
        {
            var comments = unitOfWork.CommentRepository.Get(includeProperties: "Author").OrderBy(x => x.CreationDate);
            ClaimsIdentity identity = (ClaimsIdentity)User.Identity;
            identity.Claims.ToDictionary(x => x.Type, x => x.Value).TryGetValue(ClaimTypes.NameIdentifier, out string userId);
            List<CommentDTO> newComments = comments.Select(x => new CommentDTO
            {
                Id = x.Id,
                Content = x.Content,
                CreationDate = x.CreationDate,
                PostId = x.PostId,
                IsAuthor = new Guid(userId) == x.Author.Id,
                Author = new AuthorDTO
                {
                    UserName = x.Author.UserName,
                    Id = x.Author.Id,
                    Email = x.Author.Email
                }
            }).Where(x => x.PostId == postId).ToList();
            return newComments;
        }
    }
}

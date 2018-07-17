using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Blog.Models;
using System.Threading.Tasks;
using Blog.DAL;
using System.Security.Claims;

namespace Blog.Controllers
{
    [RoutePrefix("api/comments")]
    public class CommentsController : ApiController
    {
        //private BlogContext db = new BlogContext();
        private UnitOfWork unitOfWork = new UnitOfWork();

        // GET: api/Comments
        [Route]
        public IEnumerable<Comment> GetComments()
        {
            var comments = unitOfWork.CommentRepository.Get(includeProperties: "Author").OrderByDescending(x => x.CreationDate);
            return comments;
        }

        // GET: api/Comments/5
        [Route("{id}")]
        [ResponseType(typeof(Comment))]
        public IHttpActionResult GetComment(int id)
        {
            var comment = unitOfWork.CommentRepository.GetByID(id);

            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment);
        }

        // PUT: api/Comments/5
        [Route("{id}")]
        [ResponseType(typeof(void))]
        [Authorize(Roles = "admin, user")]
        public IHttpActionResult PutComment(int id, Comment comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != comment.Id)
            {
                return BadRequest();
            }

            unitOfWork.CommentRepository.Update(comment);
            unitOfWork.Save();
            
            return Ok();
        }

        // POST: api/Comments
        [Route]
        [Authorize(Roles = "admin, user")]
        public IHttpActionResult PostComment(CommentDTO comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ClaimsIdentity identity = (ClaimsIdentity)User.Identity;
            identity.Claims.ToDictionary(x => x.Type, x => x.Value).TryGetValue(ClaimTypes.NameIdentifier, out string userId);
            Comment newComment = new Comment
            {
                Content = comment.Content,
                CreationDate = DateTime.Now,
                PostId = comment.PostId,
                AuthorId = new Guid(userId)
            };

            unitOfWork.CommentRepository.Insert(newComment);
            unitOfWork.Save();

            return Ok(newComment.Id);
        }

        // DELETE: api/Comments/5
        [ResponseType(typeof(Comment))]
        [Route("{id}")]
        [Authorize(Roles = "admin, user")]
        public IHttpActionResult DeleteComment(int id)
        {
            Comment comment = unitOfWork.CommentRepository.GetByID(id);
            unitOfWork.CommentRepository.Delete(id);
            unitOfWork.Save();

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }

    public class CommentDTO
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
        public AuthorDTO Author { get; set; }
        public int PostId { get; set; }
        public Boolean IsAuthor { get; set; }
    }
}
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

namespace Blog.Controllers
{
    [RoutePrefix("api/comments")]
    [Authorize]
    public class CommentsController : ApiController
    {
        //private BlogContext db = new BlogContext();
        private UnitOfWork unitOfWork = new UnitOfWork();

        // GET: api/Comments
        [Route]
        public IEnumerable<Comment> GetComments()
        {
            //return db.Comments;
            var comments = unitOfWork.CommentRepository.Get();
            return comments;
        }

        // GET: api/Comments/5
        [Route("{id}")]
        [ResponseType(typeof(Comment))]
        public IHttpActionResult GetComment(int id)
        {
            //Comment comment = db.Comments.Find(id);
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

            //db.Entry(comment).State = EntityState.Modified;

            //try
            //{
            //    db.SaveChanges();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!CommentExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            unitOfWork.CommentRepository.Update(comment);
            unitOfWork.Save();
            
            return Ok();
        }

        // POST: api/Comments
        [ResponseType(typeof(Comment))]
        [Route]
        public IHttpActionResult PostComment(CommentDTO comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Comment newComment = new Comment
            {
                Content = comment.Content,
                CreationDate = DateTime.Now,
                PostId = comment.PostId
            };

            //db.Comments.Add(newComment);
            //db.SaveChanges();

            unitOfWork.CommentRepository.Insert(newComment);
            unitOfWork.Save();

            return Ok(newComment.Id);
        }

        // DELETE: api/Comments/5
        [ResponseType(typeof(Comment))]
        [Route("{id}")]
        public IHttpActionResult DeleteComment(int id)
        {
            //Comment comment = db.Comments.Find(id);
            //if (comment == null)
            //{
            //    return StatusCode(HttpStatusCode.NotFound);
            //}

            //db.Comments.Remove(comment);
            //db.SaveChanges();

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
}
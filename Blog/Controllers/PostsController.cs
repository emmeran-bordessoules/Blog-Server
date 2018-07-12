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
using Blog.DAL;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Blog.Controllers
{
    [Authorize]
    [RoutePrefix("api/posts")]
    public class PostsController : ApiController
    {
        //private BlogContext db = new BlogContext();
        private UnitOfWork unitOfWork = new UnitOfWork();

        // GET: api/Posts
        [Route]
        public IEnumerable<Post> GetPosts()
        {
            //var posts = from p in db.Posts
            //            orderby p.CreationDate descending
            //            select p;

            //return posts;

            var posts = unitOfWork.PostRepository.Get().OrderByDescending(x => x.CreationDate);
            return posts;
        }

        // GET: api/Posts/5
        [Route("{id}")]
        [ResponseType(typeof(Post))]
        public IHttpActionResult GetPost(int id)
        {
            //Post post = db.Posts.Find(id);
            var post = unitOfWork.PostRepository.GetByID(id);

            if (post == null)
            {
                return NotFound();
            }

            return Ok(post);
        }

        // PUT: api/Posts/5
        [ResponseType(typeof(void))]
        [HttpPut]
        [Route("{id}")]
        public IHttpActionResult Put(int id, Post post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != post.Id)
            {
                return BadRequest();
            }

            //db.Entry(post).State = EntityState.Modified;

            //try
            //{
            //db.SaveChanges();

            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!PostExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}
            unitOfWork.PostRepository.Update(post);
            unitOfWork.Save();

            return Ok();
        }

        // POST: api/Posts
        [Route]
        [ResponseType(typeof(Post))]
        [Authorize(Roles = "admin")]
        public IHttpActionResult PostPost([FromBody] PostDTO post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Post newPost = new Post
            {
                Title = post.Title,
                Content = post.Content,
                CreationDate = DateTime.Now,
                AuthorName = User.Identity.Name
            };

            //db.Posts.Add(newPost);
            //db.SaveChanges();
            // Rollback automatisé de EF https://social.msdn.microsoft.com/Forums/en-US/32d979ce-9601-4e88-933b-3552cf1e84bd/rollback-to-savechanges?forum=adodotnetentityframework
            // https://coderwall.com/p/jnniww/why-you-shouldn-t-use-entity-framework-with-transactions

            unitOfWork.PostRepository.Insert(newPost);
            unitOfWork.Save();

            return Ok(newPost.Id);
        }

        // DELETE: api/Posts/5
        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult DeletePost(int id)
        {
            //if (post == null)
            //{
            //    return StatusCode(HttpStatusCode.NotFound);
            //}

            //db.Posts.Remove(post);
            //db.SaveChanges();
            Post post = unitOfWork.PostRepository.GetByID(id);
            unitOfWork.PostRepository.Delete(id);
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
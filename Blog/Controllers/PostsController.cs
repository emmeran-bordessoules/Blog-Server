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
using System.Security.Claims;

namespace Blog.Controllers
{
    [RoutePrefix("api/posts")]
    public class PostsController : ApiController
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        // GET: api/Posts
        [Route]
        public IEnumerable<PostDTO> GetPosts()
        {
            var posts = unitOfWork.PostRepository.Get(includeProperties: "Author").OrderByDescending(x => x.CreationDate);
            ClaimsIdentity identity = (ClaimsIdentity)User.Identity;
            identity.Claims.ToDictionary(x => x.Type, x => x.Value).TryGetValue(ClaimTypes.NameIdentifier, out string userId);
            List<PostDTO> newPosts = posts.Select(x => new PostDTO
            {
                Id = x.Id,
                Title = x.Title,
                Content = x.Content,
                CreationDate = x.CreationDate,
                IsAuthor = new Guid(userId) == x.Author.Id,
                Author = new AuthorDTO
                {
                    UserName = x.Author.UserName,
                    Id = x.Author.Id,
                    Email = x.Author.Email
                }
            }).ToList();
            return newPosts;
        }

        // GET: api/Posts/5
        [Route("{id}")]
        public PostDTO GetPost(int id)
        {
            Post post = unitOfWork.PostRepository.GetByID(id);
            User author = unitOfWork.UserRepository.GetByID(post.AuthorId);
            ClaimsIdentity identity = (ClaimsIdentity)User.Identity;
            identity.Claims.ToDictionary(x => x.Type, x => x.Value).TryGetValue(ClaimTypes.NameIdentifier, out string userId);
            PostDTO newPost = new PostDTO
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                CreationDate = post.CreationDate,
                IsAuthor = new Guid(userId) == post.Author.Id,
                Author = new AuthorDTO
                {
                    UserName = post.Author.UserName,
                    Id = post.Author.Id,
                    Email = post.Author.Email
                }
            };
            return newPost;
        }

        // PUT: api/Posts/5
        [ResponseType(typeof(void))]
        [Route("{id}")]
        [Authorize(Roles = "admin")]
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
            ClaimsIdentity identity = (ClaimsIdentity)User.Identity;
            identity.Claims.ToDictionary(x => x.Type, x => x.Value).TryGetValue(ClaimTypes.NameIdentifier, out string userId);
            Post newPost = new Post
            {
                Title = post.Title,
                Content = post.Content,
                CreationDate = DateTime.Now,
                AuthorId = new Guid(userId)
            };

            // Rollback automatisé de EF https://social.msdn.microsoft.com/Forums/en-US/32d979ce-9601-4e88-933b-3552cf1e84bd/rollback-to-savechanges?forum=adodotnetentityframework
            // https://coderwall.com/p/jnniww/why-you-shouldn-t-use-entity-framework-with-transactions

            unitOfWork.PostRepository.Insert(newPost);
            unitOfWork.Save();

            return Ok(newPost.Id);
        }

        // DELETE: api/Posts/5
        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "admin")]
        public IHttpActionResult DeletePost(int id)
        {
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

    public class AuthorDTO
    {
        public string UserName { get; set; }
        public Guid Id { get; set; }
        public string Email { get; set; }
    }

    public class PostDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Boolean IsAuthor { get; set; }
        public DateTime CreationDate { get; set; }
        public AuthorDTO Author { get; set; }
    }
}
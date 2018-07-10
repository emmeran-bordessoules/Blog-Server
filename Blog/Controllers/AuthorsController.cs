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
    [RoutePrefix("api/account")]
    public class AuthorsController : ApiController
    {
        //private BlogContext db = new BlogContext();
        private UnitOfWork unitOfWork = new UnitOfWork();
        private AuthRepository _repo = new AuthRepository();

        // GET: api/Authors
        public IEnumerable<Author> GetAuthors()
        {
            //return db.Author;
            var authors = unitOfWork.AuthorRepository.Get();
            return authors;
        }

        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(Author userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await _repo.RegisterUser(userModel);

            //IHttpActionResult errorResult = GetErrorResult(result);

            //if (errorResult != null)
            //{
            //    return errorResult;
            //}

            return Ok();
        }

        // POST api/account/logout
        [Route("logout")]
        public IHttpActionResult Logout()
        {
            return Ok();
        }

        // GET: api/Authors/5
        //[ResponseType(typeof(Author))]
        //public IHttpActionResult GetAuthor(int id)
        //{
        //    Author author = db.Authors.Find(id);
        //    if (author == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(author);
        //}

        //// PUT: api/Authors/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutAuthor(int id, Author author)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != author.Id)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(author).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!AuthorExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        //// POST: api/Authors
        //[ResponseType(typeof(Author))]
        //public IHttpActionResult PostAuthor(Author author)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.Authors.Add(author);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = author.Id }, author);
        //}

        //// DELETE: api/Authors/5
        //[ResponseType(typeof(Author))]
        //public IHttpActionResult DeleteAuthor(int id)
        //{
        //    Author author = db.Authors.Find(id);
        //    if (author == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Authors.Remove(author);
        //    db.SaveChanges();

        //    return Ok(author);
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        //private bool AuthorExists(int id)
        //{
        //    return db.Authors.Count(e => e.Id == id) > 0;
        //}
    }
}
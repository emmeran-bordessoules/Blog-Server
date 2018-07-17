using System.Collections.Generic;
using System.Web.Http;
using Blog.Models;
using Blog.DAL;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using static Blog.DAL.AuthRepository;
using System.Linq;
using System.Net.Http;
using Microsoft.AspNet.Identity.Owin;

namespace Blog.Controllers
{
    [RoutePrefix("api/account")]
    public class UsersController : ApiController
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private AuthRepository _repo = new AuthRepository();

        // GET: api/Authors
        [Route]
        public IEnumerable<User> GetAuthors()
        {
            var users = unitOfWork.UserRepository.Get();
            return users;
        }

        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(Blog.DAL.AuthRepository.UserDTO userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await _repo.RegisterUser(userModel);
            unitOfWork.Save();
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
        //    Author user = db.Authors.Find(id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(user);
        //}

        //// PUT: api/Authors/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutAuthor(int id, Author user)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != user.Id)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(user).State = EntityState.Modified;

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
        //public IHttpActionResult PostAuthor(Author user)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.Authors.Add(user);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = user.Id }, user);
        //}

        //// DELETE: api/Authors/5
        //[ResponseType(typeof(Author))]
        //public IHttpActionResult DeleteAuthor(int id)
        //{
        //    Author user = db.Authors.Find(id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Authors.Remove(user);
        //    db.SaveChanges();

        //    return Ok(user);
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
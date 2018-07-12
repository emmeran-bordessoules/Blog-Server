using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Blog.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;

namespace Blog.DAL
{
    public class AuthRepository : IDisposable
    {
        private BlogContext _ctx;

        private UserManager<Author> _userManager;

        public AuthRepository()
        {
            _ctx = new BlogContext();
            _userManager = new UserManager<Author>(new UserStore<Author>(_ctx));
        }

        public async Task<IdentityResult> RegisterUser(AuthorDTO userModel)
        {
            Author user = new Author
            {
                UserName = userModel.UserName,
                Email = userModel.UserName
            };

            var result = await _userManager.CreateAsync(user, userModel.Password);

            _userManager.AddToRole(user.Id, "admin" );

            return result;
        }

        public async Task<IdentityUser> FindUser(string userName, string password)
        {
            IdentityUser user = await _userManager.FindAsync(userName, password);

            return user;
        }

        public void Dispose()
        {
            _ctx.Dispose();
            _userManager.Dispose();

        }

        public class AuthorDTO
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }
    }
}
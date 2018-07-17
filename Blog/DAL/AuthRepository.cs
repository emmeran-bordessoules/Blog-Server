using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Blog.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;
using System.Data.Entity.Validation;

namespace Blog.DAL
{
    public class AuthRepository : IDisposable
    {
        private BlogContext _ctx;

        private UserManager<User, Guid> _userManager;

        public AuthRepository()
        {
            _ctx = new BlogContext();
            _userManager = new UserManager<User, Guid>(new UserStoreGuidPk(_ctx));
        }

        public async Task<IdentityResult> RegisterUser(UserDTO userModel)
        {
            User user = new User
            {
                UserName = userModel.UserName,
                Email = userModel.UserName
            };

            var result = await _userManager.CreateAsync(user, userModel.Password);
            _userManager.AddToRole(user.Id, userModel.Role);

            return result;
        }

        public async Task<User> FindUser(string userName, string password)
        {
            User user = await _userManager.FindAsync(userName, password);

            return user;
        }

        public void Dispose()
        {
            _ctx.Dispose();
            _userManager.Dispose();

        }

        public class UserDTO
        {
            public string UserName { get; set; }
            public string Password { get; set; }
            public string Role { get; set; }
        }
    }
}
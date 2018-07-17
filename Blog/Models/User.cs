using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Blog.Models
{
    public class UserRoleGuidPk : IdentityUserRole<Guid>
    {
    }

    public class UserClaimGuidPk : IdentityUserClaim<Guid>
    {
    }

    public class UserLoginGuidPk : IdentityUserLogin<Guid>
    {
    }

    public class RoleGuidPk : IdentityRole<Guid, UserRoleGuidPk>
    {
        public RoleGuidPk() { }
        public RoleGuidPk(string name) { Name = name; }
    }

    public class UserStoreGuidPk : UserStore<User, RoleGuidPk, Guid, UserLoginGuidPk, UserRoleGuidPk, UserClaimGuidPk>
    {
        public UserStoreGuidPk(BlogContext context)
            : base(context)
        {
        }
    }

    public class RoleStoreGuidPk : RoleStore<RoleGuidPk, Guid, UserRoleGuidPk>
    {
        public RoleStoreGuidPk(BlogContext context)
            : base(context)
        {
        }
    }

    public class User : IdentityUser<Guid, UserLoginGuidPk, UserRoleGuidPk, UserClaimGuidPk>
    {
        public User()
        {
            base.Id = Guid.NewGuid();
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User, Guid> user, string authenticationType)
        {
            var userIdentity = await user.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
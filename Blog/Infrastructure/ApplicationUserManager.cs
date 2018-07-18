using Blog.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Infrastructure
{
    public class ApplicationUserManager : UserManager<User, Guid>
    {
        public ApplicationUserManager(UserStoreGuidPk store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var appDbContext = context.Get<BlogContext>();
            var appUserManager = new ApplicationUserManager(new UserStoreGuidPk(appDbContext));

            return appUserManager;
        }
    }
}
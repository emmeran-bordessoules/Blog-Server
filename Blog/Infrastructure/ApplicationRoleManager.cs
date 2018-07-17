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
    public class ApplicationRoleManager : RoleManager<RoleGuidPk, Guid>
    {
        public ApplicationRoleManager(IRoleStore<RoleGuidPk, Guid> roleStore)
            : base(roleStore)
        {
        }

        public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options, IOwinContext context)
        {
            var appRoleManager = new ApplicationRoleManager(new RoleStoreGuidPk(context.Get<BlogContext>()));

            return appRoleManager;
        }
    }
}
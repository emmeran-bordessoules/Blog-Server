using Blog.DAL;
using Blog.Infrastructure;
using Blog.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Blog.Provider
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            return Task.FromResult<object>(null);
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            ApplicationRoleManager roleMngr = new ApplicationRoleManager(new RoleStoreGuidPk(new BlogContext()));
            var roles = roleMngr.Roles.ToDictionary(x => x.Id, x => x.Name);

            using (AuthRepository _repo = new AuthRepository())
            {
                User user = await _repo.FindUser(context.UserName, context.Password);
            
                if (user == null)
                {
                    context.SetError("invalid_grant", "The user name or password is incorrect.");
                    return;
                }

                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));

                foreach (UserRoleGuidPk role in user.Roles)
                {
                    if (roles.TryGetValue(role.RoleId, out string userRole))
                        identity.AddClaim(new Claim(ClaimTypes.Role, userRole));
                }

                context.Validated(identity);
            }
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            ApplicationRoleManager roleMngr = new ApplicationRoleManager(new RoleStoreGuidPk(new BlogContext()));
            var roles = roleMngr.Roles.ToDictionary(x => x.Id, x => x.Name);
            User user = unitOfWork.UserRepository.GetByID(new Guid(context.Identity.GetUserId()));

            foreach (UserRoleGuidPk role in user.Roles)
            {
                if (roles.TryGetValue(role.RoleId, out string userRole))
                    context.AdditionalResponseParameters.Add("role", userRole);
            }

            return Task.FromResult<object>(null);
        }
    }
}
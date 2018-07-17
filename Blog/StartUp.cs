using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Linq;
using System.Web;
using System.Web.Http;
using Blog.Provider;
using Microsoft.Owin.Security;
using Microsoft.Owin.Cors;
using System.Threading.Tasks;
using System.Collections.Generic;
using Blog.Infrastructure;
using Blog.Models;

[assembly: OwinStartup(typeof(Blog.StartUp))]
namespace Blog
{
    public class StartUp
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureOAuth(app);

            HttpConfiguration config = new HttpConfiguration();

            WebApiConfig.Register(config);

            app.UseWebApi(config);
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions OAuthAuthorizationServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(60),
                Provider = new SimpleAuthorizationServerProvider(),
                AuthenticationMode = AuthenticationMode.Active
            };

            app.UseOAuthAuthorizationServer(OAuthAuthorizationServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}
using Microsoft.AspNet.Identity.EntityFramework;

namespace Blog.Models
{
    public class AuthContext : IdentityDbContext<IdentityUser>
    {
        public AuthContext()
            : base("AuthContext")
        {

        }
    }
}
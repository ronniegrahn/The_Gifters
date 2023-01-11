using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace The_Gifters.Models
{
    public class UsersService
    {
        public UsersService(IdentityDbContext identityDbContext)
        {
            identityDbContext.Database.EnsureCreated();
        }
    }
}

using Authentication.Infrastructure.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Infrastructure.Persistence
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        protected AppDbContext()
        {
        }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}
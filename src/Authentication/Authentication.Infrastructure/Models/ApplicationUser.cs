using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Authentication.Infrastructure.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        [Required]
        [StringLength(100)]
        public string FirstName { get; private set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; private set; }

        private ApplicationUser()
        {
        }

        public ApplicationUser(string userName, string email, string firstName, string lastName)
        {
            UserName = userName;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
        }

        public static ApplicationUser Create(string userName, string email, string firstName, string lastName)
        {
            return new ApplicationUser(userName, email, firstName, lastName);
        }
    }
}
using Authentication.Infrastructure.Models;
using MediatR;
using UsedCables.Infrastructure.Helpers.ResponseHelper;

namespace Authentication.Infrastructure.Features.Commands.Register
{
    public class RegisterCommand : IRequest<Result<ApplicationUser>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public List<RegisterRoles> RegisterRoles { get; set; }
    }

    public class RegisterRoles
    {
        public string Role { get; set; }
    }
}
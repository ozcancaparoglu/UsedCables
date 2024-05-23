using Authentication.Infrastructure.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using UsedCables.Infrastructure.Helpers.ResponseHelper;

namespace Authentication.Infrastructure.Features.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<ApplicationUser>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<RegisterCommandHandler> _logger;

        public RegisterCommandHandler(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ILogger<RegisterCommandHandler> logger)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Result<ApplicationUser>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Registering user");

            var existingUser = await _userManager.FindByEmailAsync(request.Email);

            if (existingUser != null)
            {
                _logger.LogError("User already exists");
                return await Result<ApplicationUser>.FailureAsync("User already exists");
            } 

            var user = ApplicationUser.Create(request.UserName, request.Email, request.FirstName, request.LastName);

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                _logger.LogError("Failed to create user");
                return await Result<ApplicationUser>.FailureAsync(result.Errors.Select(x => x.Description));
            }

            var roleNames = request.RegisterRoles.Select(x => x.Role).ToList();

            foreach (var role in roleNames)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    _logger.LogError("Role does not exist");
                    return await Result<ApplicationUser>.FailureAsync("Role does not exist");
                }

                var roleResult = await _userManager.AddToRoleAsync(user, role);

                if (!roleResult.Succeeded)
                {
                    _logger.LogError("Failed to add user to role");
                    return await Result<ApplicationUser>.FailureAsync(roleResult.Errors.Select(x => x.Description));
                }

            }

            return await Result<ApplicationUser>.SuccessAsync(user);
        }
    }
}

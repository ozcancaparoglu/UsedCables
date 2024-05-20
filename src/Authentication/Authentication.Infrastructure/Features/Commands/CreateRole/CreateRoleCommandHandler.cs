using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using UsedCables.Infrastructure.Helpers.ResponseHelper;

namespace Authentication.Infrastructure.Features.Commands.CreateRole
{
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, Result<bool>>
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<CreateRoleCommandHandler> _logger;

        public CreateRoleCommandHandler(RoleManager<IdentityRole> roleManager, ILogger<CreateRoleCommandHandler> logger)
        {
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Result<bool>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            foreach (var role in request.Roles)
            {
                if (await _roleManager.RoleExistsAsync(role))
                    _logger.LogError("Role already exists role: {Role}", role);

                var result = await _roleManager.CreateAsync(new IdentityRole(role));

                if (!result.Succeeded)
                    _logger.LogError("Failed to create role: {Role}", role);
            }

            return await Result<bool>.SuccessAsync();
        }
    }
}
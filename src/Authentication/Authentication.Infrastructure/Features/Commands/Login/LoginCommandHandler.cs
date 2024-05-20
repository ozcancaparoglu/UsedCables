using Authentication.Infrastructure.Models;
using MediatR;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using UsedCables.Infrastructure.Helpers.Authentication.JwtManager;

namespace Authentication.Infrastructure.Features.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, AccessTokenResponse>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtTokenManager _jwtTokenManager;

        public LoginCommandHandler(UserManager<ApplicationUser> userManager, IJwtTokenManager jwtTokenManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _jwtTokenManager = jwtTokenManager ?? throw new ArgumentNullException(nameof(jwtTokenManager));
        }

        public async Task<AccessTokenResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
                throw new UnauthorizedAccessException("Invalid credentials");

            var result = await _userManager.CheckPasswordAsync(user, request.Password);

            if (!result)
                throw new UnauthorizedAccessException("Invalid credentials");

            var roles = await _userManager.GetRolesAsync(user);
           
            return new AccessTokenResponse
            {
                AccessToken = _jwtTokenManager.GenerateJwtToken(user.UserName, roles),
                ExpiresIn = DateTime.UtcNow.AddMinutes(15).Ticks,
                RefreshToken = _jwtTokenManager.GenerateRefreshToken(user.UserName)
            };
        }
    }
}
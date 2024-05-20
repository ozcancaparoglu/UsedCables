using System.Security.Claims;

namespace UsedCables.Infrastructure.Helpers.Authentication.JwtManager
{
    public interface IJwtTokenManager
    {
        /// <summary>
        /// Using for both token generation and refresh token generation
        /// If roles are not provided, token will be valid for 15 minutes
        /// </summary>
        /// <param name="username">User Name</param>
        /// <param name="roles">User Roles</param>
        /// <returns>String Token</returns>
        string GenerateJwtToken(string username, IList<string>? roles = null);
        string GenerateRefreshToken(string username);
        ClaimsPrincipal GetPrincipalFromToken(string token);
    }
}
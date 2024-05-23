using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UsedCables.Infrastructure.Configuration;

namespace UsedCables.Infrastructure.Helpers.Authentication.JwtManager
{
    public class JwtTokenManager : IJwtTokenManager
    {
        private readonly JwtConfigurationOptions _configuration;

        public JwtTokenManager(IOptions<JwtConfigurationOptions> options)
        {
            _configuration = options.Value;
        }

        /// <summary>
        /// Using token generation
        /// </summary>
        /// <param name="username">User Name</param>
        /// <param name="roles">User Roles</param>
        /// <returns>String Token</returns>
        public string GenerateJwtToken(string username, IList<string>? roles = null)
        {
            var key = _configuration.Key;
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            DateTime? expires = DateTime.UtcNow.AddMinutes(15);

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new(ClaimTypes.NameIdentifier, username)
                }),
                Issuer = _configuration.Issuer,
                Audience = _configuration.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
            };

            if (roles != null)
            {
                foreach (var role in roles)
                {
                    tokenDescriptor.Subject.AddClaim(new Claim(ClaimTypes.Role, role));
                }
            }

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public ClaimsPrincipal GetPrincipalFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.Key);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = _configuration.Issuer,
                ValidAudience = _configuration.Audience,
                ValidateLifetime = true
            };

            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

            return principal;
        }

        public string GenerateRefreshToken(string username)
        {
            var key = _configuration.Key;
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            DateTime? expires = DateTime.UtcNow.AddHours(12);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new(ClaimTypes.NameIdentifier, username)
                }),
                Issuer = _configuration.Issuer,
                Audience = _configuration.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

        }
    }
}
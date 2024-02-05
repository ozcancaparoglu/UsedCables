using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Text;
using UsedCables.Infrastructure.Configuration;
using UsedCables.Infrastructure.Entities;

namespace UsedCables.Infrastructure.Middlewares
{
    public class JwtMiddleware : IMiddleware
    {
        private readonly JwtConfigurationOptions _configuration;

        public JwtMiddleware(IOptions<JwtConfigurationOptions> options)
        {
            _configuration = options.Value;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            string authorizationHeader = context.Request.Headers["Authorization"];

            if (string.IsNullOrEmpty(authorizationHeader))
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return;
            }

            if (!authorizationHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return;
            }

            string token = authorizationHeader.Substring("Bearer ".Length).Trim();

            if (!string.IsNullOrEmpty(token))
                AttachAccountToContext(context, token);

            await next(context);
        }

        private void AttachAccountToContext(HttpContext context, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration.Key);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = _configuration.Issuer,
                    ValidAudience = _configuration.Audience,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                var userInfo = new UserInfo
                {
                    UserId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value),
                    Username = jwtToken.Claims.First(x => x.Type == "username").Value,
                    Email = jwtToken.Claims.First(x => x.Type == "email").Value
                };

                context.Items["User"] = userInfo;

            }
            catch
            {
                // do nothing if jwt validation fails
                // account is not attached to context so request won't have access to secure routes
            }
        }
    }
}
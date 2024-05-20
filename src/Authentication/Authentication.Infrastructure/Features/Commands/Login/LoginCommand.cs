using MediatR;
using Microsoft.AspNetCore.Authentication.BearerToken;

namespace Authentication.Infrastructure.Features.Commands.Login
{
    public class LoginCommand : IRequest<AccessTokenResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
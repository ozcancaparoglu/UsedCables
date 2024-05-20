using MediatR;
using UsedCables.Infrastructure.Helpers.ResponseHelper;

namespace Authentication.Infrastructure.Features.Commands.CreateRole
{
    public class CreateRoleCommand : IRequest<Result<bool>>
    {
        public List<string> Roles { get; set; }
    }
}
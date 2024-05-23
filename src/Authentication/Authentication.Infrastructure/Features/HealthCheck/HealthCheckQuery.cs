using MediatR;
using UsedCables.Infrastructure.Helpers.ResponseHelper;

namespace Authentication.Infrastructure.Features.HealthCheck
{
    public class HealthCheckQuery : IRequest<Result<object>>
    {
        public string Message { get; set; } = "I am fine, mind your own business.";
    }
}

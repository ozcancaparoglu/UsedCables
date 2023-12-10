using MediatR;
using UsedCables.Infrastructure.Helpers.ResponseHelper;

namespace ProductService.Application.Features.HealthCheck
{
    public class HealthCheckQuery : IRequest<Result<object>>
    {
        public string Message { get; set; } = "I am fine, mind your own business.";
    }
}
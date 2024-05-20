using MediatR;
using ProductService.Application.ApiResponses.Queries.GetProduct;
using UsedCables.Infrastructure.Helpers.ResponseHelper;

namespace ProductService.Application.Features.Queries.GetProduct
{
    public class GetProductQuery : IRequest<Result<GetProductResponse>>
    {
        public Guid Id { get; set; }
    }
}
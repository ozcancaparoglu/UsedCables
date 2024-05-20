using AutoMapper;
using MediatR;
using ProductService.Application.ApiResponses.Queries.GetProduct;
using ProductService.Application.Services.Contracts;
using UsedCables.Infrastructure.Helpers.ResponseHelper;

namespace ProductService.Application.Features.Queries.GetProduct
{
    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, Result<GetProductResponse>>
    {
        private readonly IAsyncProductService _productService;
        private readonly IMapper _mapper;

        public GetProductQueryHandler(IAsyncProductService productService, IMapper mapper)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result<GetProductResponse>> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var entity = await _productService.GetByIdAsync(request.Id);

            if (entity == null)
                return await Result<GetProductResponse>.FailureAsync("Product not found.");

            var response = _mapper.Map<GetProductResponse>(entity);

            return await Result<GetProductResponse>.SuccessAsync(response);
        }
    }
}
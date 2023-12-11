using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ProductService.Application.Services.Contracts;
using ProductService.Domain.ProductAggregate;
using UsedCables.Infrastructure.Helpers.ResponseHelper;

namespace ProductService.Application.Features.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<Unit>>
    {
        private readonly IAsyncProductService _productService;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateProductCommandHandler> _logger;

        public CreateProductCommandHandler(IAsyncProductService productService, IMapper mapper, ILogger<CreateProductCommandHandler> logger)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Result<Unit>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Product>(request);

            var id = await _productService.CreateAsync(entity);

            _logger.LogInformation($"Product with id: {id} has been created.");

            return await Result<Unit>.SuccessAsync();
        }
    }
}
﻿using AutoMapper;
using MediatR;
using ProductService.Application.Services.Contracts;
using ProductService.Domain.ProductAggregate;
using UsedCables.Infrastructure.Helpers.ResponseHelper;

namespace ProductService.Application.Features.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<bool>>
    {
        private readonly IAsyncProductService _productService;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(IAsyncProductService productService, IMapper mapper)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result<bool>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Product>(request);

           await _productService.CreateAsync(entity);

            return await Result<bool>.SuccessAsync();
        }
    }
}
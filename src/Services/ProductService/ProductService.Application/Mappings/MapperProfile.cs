using AutoMapper;
using ProductService.Application.ApiResponses.Queries.GetProduct;
using ProductService.Application.Features.Commands.CreateProduct;
using ProductService.Application.Features.Commands.ProductCommon;
using ProductService.Domain.ProductAggregate;

namespace ProductService.Application.Mappings
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CreateProductCommand, Product>();
            CreateMap<ProductAttributesCommand, ProductAttributes>();
            CreateMap<ProductPicturesCommand, ProductPictures>();
            CreateMap<ProductSellersCommand, ProductSellers>();
            CreateMap<Product, GetProductResponse>();
        }
    }
}
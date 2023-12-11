using MediatR;
using UsedCables.Infrastructure.Helpers.ResponseHelper;

namespace ProductService.Application.Features.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest<Result<Unit>>
    {
        public int ParentProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ProductAttributesCommand>? ProductAttributes { get; set; }
        public List<ProductPicturesCommand>? ProductPictures { get; set; }
        public List<ProductSellersCommand>? ProductSellers { get; set; }
    }

    public class ProductAttributesCommand
    {
        public int ProductId { get; set; }
        public int AttributeId { get; set; }
        public int AttributeValueId { get; set; }
        public string AttributeName { get; set; }
        public string AttributeValueName { get; set; }
    }

    public class ProductPicturesCommand
    {
        public string? PictureUrl { get; set; }
        public bool IsApproved { get; set; }
        public int Order { get; set; }
    }

    public class ProductSellersCommand
    {
        public int SellerId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
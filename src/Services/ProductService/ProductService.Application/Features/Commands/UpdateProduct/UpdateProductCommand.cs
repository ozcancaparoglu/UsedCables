using MediatR;
using ProductService.Application.Features.Commands.ProductCommon;
using UsedCables.Infrastructure.Helpers.ResponseHelper;

namespace ProductService.Application.Features.Commands.UpdateProduct
{
    public class UpdateProductCommand : IRequest<Result<bool>>
    {
        public int ParentProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ProductAttributesCommand>? ProductAttributes { get; set; }
        public List<ProductPicturesCommand>? ProductPictures { get; set; }
        public List<ProductSellersCommand>? ProductSellers { get; set; }
    }
}
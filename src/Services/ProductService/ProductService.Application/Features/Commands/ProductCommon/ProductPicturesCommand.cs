namespace ProductService.Application.Features.Commands.ProductCommon
{
    public class ProductPicturesCommand
    {
        public string? PictureUrl { get; set; }
        public bool IsApproved { get; set; }
        public int Order { get; set; }
    }
}

namespace ProductService.Application.Features.Commands.ProductCommon
{
    public class ProductSellersCommand
    {
        public int SellerId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}

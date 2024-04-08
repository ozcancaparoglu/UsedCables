namespace ProductService.Application.Features.Commands.ProductCommon
{
    public class ProductAttributesCommand
    {
        public int AttributeId { get; set; }
        public int AttributeValueId { get; set; }
        public string AttributeName { get; set; }
        public string AttributeValueName { get; set; }
    }
}

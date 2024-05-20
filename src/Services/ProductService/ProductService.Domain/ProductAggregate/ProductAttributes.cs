using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UsedCables.Infrastructure.Entities;

namespace ProductService.Domain.ProductAggregate
{
    public class ProductAttributes : EntityBase
    {
        public Guid ProductId { get; private set; }

        [ForeignKey("ProductId")]
        public virtual Product? Product { get; private set; }

        public Guid AttributeId { get; private set; }

        public Guid AttributeValueId { get; private set; }

        [Required]
        [MaxLength(500)]
        public string AttributeName { get; private set; }

        [Required]
        [MaxLength(500)]
        public string AttributeValueName { get; private set; }

        protected ProductAttributes()
        {
        }

        public ProductAttributes(Guid productId, Guid attributeId, Guid attributeValueId, string attributeName, string attributeValueName)
        {
            ProductId = productId;
            AttributeId = attributeId;
            AttributeValueId = attributeValueId;
            AttributeName = attributeName;
            AttributeValueName = attributeValueName;
        }

        #region Methods

        public void Update(Guid productId, Guid attributeId, Guid attributeValueId, string attributeName, string attributeValueName)
        {
            ProductId = productId;
            AttributeId = attributeId;
            AttributeValueId = attributeValueId;
            AttributeName = attributeName;
            AttributeValueName = attributeValueName;
        }

        public void Deleted()
        {
            Delete();
        }

        public void Restore()
        {
            Activate();
        }

        public void ChangeAttributeName(string attributeName)
        {
            AttributeName = attributeName;
        }

        public void ChangeAttributeValueName(string attributeValueName)
        {
            AttributeValueName = attributeValueName;
        }

        public void ChangeAttribute(Guid attributeId)
        {
            AttributeId = attributeId;
        }

        public void ChangeAttributeValue(Guid attributeValueId)
        {
            AttributeValueId = attributeValueId;
        }

        #endregion
    }
}
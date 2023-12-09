using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UsedCables.Infrastructure.Entities;

namespace ProductService.Domain.ProductAggregate
{
    public class ProductAttributes : EntityBase
    {
        public int ProductId { get; private set; }

        [ForeignKey("ProductId")]
        public virtual Product? Product { get; private set; }

        public int AttributeId { get; private set; }

        public int AttributeValueId { get; private set; }

        [Required]
        [MaxLength(500)]
        public string AttributeName { get; private set; }

        [Required]
        [MaxLength(500)]
        public string AttributeValueName { get; private set; }

        protected ProductAttributes()
        {
        }

        public ProductAttributes(int productId, int attributeId, int attributeValueId, string attributeName, string attributeValueName)
        {
            ProductId = productId;
            AttributeId = attributeId;
            AttributeValueId = attributeValueId;
            AttributeName = attributeName;
            AttributeValueName = attributeValueName;
        }

        #region Methods

        public void Update(int productId, int attributeId, int attributeValueId, string attributeName, string attributeValueName)
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

        public void ChangeAttribute(int attributeId)
        {
            AttributeId = attributeId;
        }

        public void ChangeAttributeValue(int attributeValueId)
        {
            AttributeValueId = attributeValueId;
        }

        #endregion
    }
}
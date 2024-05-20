using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UsedCables.Infrastructure.Entities;

namespace ProductService.Domain.ProductAggregate
{
    public class Product : EntityBase
    {
        [Required]
        public Guid ParentProductId { get; private set; }

        [ForeignKey("ParentProductId")]
        public virtual ParentProduct? ParentProduct { get; private set; }

        [Required]
        [MaxLength(500)]
        public string Name { get; private set; }

        [MaxLength(500)]
        public string? Description { get; private set; }

        private readonly List<ProductAttributes> _productAttributes;
        public IReadOnlyCollection<ProductAttributes> ProductAttributes => _productAttributes;


        private readonly List<ProductPictures> _productPictures;
        public IReadOnlyCollection<ProductPictures> ProductPictures => _productPictures;


        private readonly List<ProductSellers> _productSellers;
        public IReadOnlyCollection<ProductSellers> ProductSellers => _productSellers;

        protected Product()
        {
            _productAttributes = [];
            _productPictures = [];
            _productSellers = [];
        }

        public Product(Guid parentProductId, string name, string? description)
        {
            ParentProductId = parentProductId;
            Name = name;
            Description = description;
        }

        #region Methods

        public void Update(Guid parentProductId, string name, string? description)
        {
            ParentProductId = parentProductId;
            Name = name;
            Description = description;
        }

        public void Deleted()
        {
            Delete();
        }

        public void Restore()
        {
            Activate();
        }

        public void ChangeName(string name)
        {
            Name = name;
        }

        public void ChangeDescription(string? description)
        {
            Description = description;
        }

        public void ChangeParentProduct(Guid parentProductId)
        {
            ParentProductId = parentProductId;
        }

        #endregion Methods

        #region ProductAttributes

        public void SetProductAttributes(List<ProductAttributes> productAttributes)
        {
            foreach (var productAttribute in productAttributes)
            {
                _productAttributes.Add(productAttribute);
            }
        }

        public void AddProductAttributes(Guid attributeId, Guid attributeValueId, string attributeName, string attributeValueName)
        {
            var productAttributes = new ProductAttributes(Id, attributeId, attributeValueId, attributeName, attributeValueName);
            _productAttributes.Add(productAttributes);
        }

        public void UpdateProductAttributes(Guid productAttributesId, Guid attributeId, Guid attributeValueId, string attributeName, string attributeValueName)
        {
            var productAttributes = _productAttributes.Find(x => x.Id == productAttributesId);

            productAttributes?.Update(Id, attributeId, attributeValueId, attributeName, attributeValueName);
        }

        public void RemoveProductAttributes(Guid productAttributesId)
        {
            var productAttributes = _productAttributes.Find(x => x.Id == productAttributesId);

            productAttributes?.Deleted();
        }

        public void RestoreProductAttributes(Guid productAttributesId)
        {
            var productAttributes = _productAttributes.Find(x => x.Id == productAttributesId);

            productAttributes?.Restore();
        }

        #endregion ProductAttributes

        #region ProductPictures

        public void SetProductPictures(List<ProductPictures> productPictures)
        {
            foreach (var productPicture in productPictures)
            {
                _productPictures.Add(productPicture);
            }
        }   

        public void AddProductPictures(string? pictureUrl, bool isApproved, int order)
        {
            var productPictures = new ProductPictures(Id, pictureUrl, isApproved, order);
            _productPictures.Add(productPictures);
        }

        public void UpdateProductPictures(Guid productPicturesId, string? pictureUrl, bool isApproved, int order)
        {
            var productPictures = _productPictures.Find(x => x.Id == productPicturesId);

            productPictures?.Update(Id, pictureUrl, isApproved, order);
        }

        public void RemoveProductPictures(Guid productPicturesId)
        {
            var productPictures = _productPictures.Find(x => x.Id == productPicturesId);

            productPictures?.Deleted();
        }

        public void RestoreProductPictures(Guid productPicturesId)
        {
            var productPictures = _productPictures.Find(x => x.Id == productPicturesId);

            productPictures?.Restore();
        }

        #endregion ProductPictures

        #region ProductSellers

        public void SetProductSellers(List<ProductSellers> productSellers)
        {
            foreach (var productSeller in productSellers)
            {
                _productSellers.Add(productSeller);
            }
        }

        public void AddProductSellers(Guid sellerId, decimal price, int quantity)
        {
            var productSellers = new ProductSellers(Id, sellerId, price, quantity);
            _productSellers.Add(productSellers);
        }

        public void UpdateProductSellers(Guid productSellersId, Guid sellerId, decimal price, int quantity)
        {
            var productSellers = _productSellers.Find(x => x.Id == productSellersId);

            productSellers?.Update(Id, sellerId, price, quantity);
        }

        public void RemoveProductSellers(Guid productSellersId)
        {
            var productSellers = _productSellers.Find(x => x.Id == productSellersId);

            productSellers?.Deleted();
        }

        public void RestoreProductSellers(Guid productSellersId)
        {
            var productSellers = _productSellers.Find(x => x.Id == productSellersId);

            productSellers?.Restore();
        }

        #endregion ProductSellers
    }
}
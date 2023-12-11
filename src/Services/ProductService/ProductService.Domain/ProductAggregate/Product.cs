using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UsedCables.Infrastructure.Entities;

namespace ProductService.Domain.ProductAggregate
{
    public class Product : EntityBase
    {
        [Required]
        public int ParentProductId { get; private set; }

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

        public Product(int parentProductId, string name, string? description)
        {
            ParentProductId = parentProductId;
            Name = name;
            Description = description;
        }

        #region Methods

        public void Update(int parentProductId, string name, string? description)
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

        public void ChangeParentProduct(int parentProductId)
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

        public void AddProductAttributes(int attributeId, int attributeValueId, string attributeName, string attributeValueName)
        {
            var productAttributes = new ProductAttributes(Id, attributeId, attributeValueId, attributeName, attributeValueName);
            _productAttributes.Add(productAttributes);
        }

        public void UpdateProductAttributes(int productAttributesId, int attributeId, int attributeValueId, string attributeName, string attributeValueName)
        {
            var productAttributes = _productAttributes.Find(x => x.Id == productAttributesId);

            if (productAttributes != null)
                productAttributes.Update(Id, attributeId, attributeValueId, attributeName, attributeValueName);
        }

        public void RemoveProductAttributes(int productAttributesId)
        {
            var productAttributes = _productAttributes.Find(x => x.Id == productAttributesId);

            if (productAttributes != null)
                productAttributes.Deleted();
        }

        public void RestoreProductAttributes(int productAttributesId)
        {
            var productAttributes = _productAttributes.Find(x => x.Id == productAttributesId);

            if (productAttributes != null)
                productAttributes.Restore();
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

        public void UpdateProductPictures(int productPicturesId, string? pictureUrl, bool isApproved, int order)
        {
            var productPictures = _productPictures.Find(x => x.Id == productPicturesId);

            if (productPictures != null)
                productPictures.Update(Id, pictureUrl, isApproved, order);
        }

        public void RemoveProductPictures(int productPicturesId)
        {
            var productPictures = _productPictures.Find(x => x.Id == productPicturesId);

            if (productPictures != null)
                productPictures.Deleted();
        }

        public void RestoreProductPictures(int productPicturesId)
        {
            var productPictures = _productPictures.Find(x => x.Id == productPicturesId);

            if (productPictures != null)
                productPictures.Restore();
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

        public void AddProductSellers(int sellerId, decimal price, int quantity)
        {
            var productSellers = new ProductSellers(Id, sellerId, price, quantity);
            _productSellers.Add(productSellers);
        }

        public void UpdateProductSellers(int productSellersId, int sellerId, decimal price, int quantity)
        {
            var productSellers = _productSellers.Find(x => x.Id == productSellersId);

            if (productSellers != null)
                productSellers.Update(Id, sellerId, price, quantity);
        }

        public void RemoveProductSellers(int productSellersId)
        {
            var productSellers = _productSellers.Find(x => x.Id == productSellersId);

            if (productSellers != null)
                productSellers.Deleted();
        }

        public void RestoreProductSellers(int productSellersId)
        {
            var productSellers = _productSellers.Find(x => x.Id == productSellersId);

            if (productSellers != null)
                productSellers.Restore();
        }

        #endregion ProductSellers
    }
}
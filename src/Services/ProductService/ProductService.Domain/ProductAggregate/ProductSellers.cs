using System.ComponentModel.DataAnnotations.Schema;
using UsedCables.Infrastructure.Entities;

namespace ProductService.Domain.ProductAggregate
{
    public class ProductSellers : EntityBase
    {
        public Guid ProductId { get; private set; }

        [ForeignKey("ProductId")]
        public virtual Product? Product { get; private set; }

        public Guid SellerId { get; private set; }

        public decimal Price { get; private set; }

        public int Quantity { get; private set; }

        protected ProductSellers()
        {
        }

        public ProductSellers(Guid productId, Guid sellerId, decimal price, int quantity)
        {
            ProductId = productId;
            SellerId = sellerId;
            Price = price;
            Quantity = quantity;
        }

        #region Methods

        public void Update(Guid productId, Guid sellerId, decimal price, int quantity)
        {
            ProductId = productId;
            SellerId = sellerId;
            Price = price;
            Quantity = quantity;
        }

        public void Deleted()
        {
            Delete();
        }

        public void Restore()
        {
            Activate();
        }

        public void ChangePrice(decimal price)
        {
            Price = price;
        }

        public void ChangeQuantity(int quantity)
        {
            Quantity = quantity;
        }

        #endregion Methods
    }
}
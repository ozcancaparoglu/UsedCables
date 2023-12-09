using System.ComponentModel.DataAnnotations.Schema;
using UsedCables.Infrastructure.Entities;

namespace ProductService.Domain.ProductAggregate
{
    public class ProductPictures : EntityBase
    {
        public int ProductId { get; private set; }

        [ForeignKey("ProductId")]
        public virtual Product? Product { get; private set; }

        public string? PictureUrl { get; private set; }
        public bool IsApproved { get; private set; }
        public int Order { get; private set; }

        protected ProductPictures()
        {
        }

        public ProductPictures(int productId, string? pictureUrl, bool isApproved, int order)
        {
            ProductId = productId;
            PictureUrl = pictureUrl;
            IsApproved = isApproved;
            Order = order;
        }

        #region Methods

        public void Update(int productId, string? pictureUrl, bool isApproved, int order)
        {
            ProductId = productId;
            PictureUrl = pictureUrl;
            IsApproved = isApproved;
            Order = order;
        }

        public void Deleted()
        {
            Delete();
        }

        public void Restore()
        {
            Activate();
        }

        public void ChangePictureUrl(string? pictureUrl)
        {
            PictureUrl = pictureUrl;
        }

        public void ChangeIsApproved(bool isApproved)
        {
            IsApproved = isApproved;
        }

        public void ChangeOrder(int order)
        {
            Order = order;
        }

        #endregion Methods
    }
}
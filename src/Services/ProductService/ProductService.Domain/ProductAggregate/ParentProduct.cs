using System.ComponentModel.DataAnnotations;
using UsedCables.Infrastructure.Entities;

namespace ProductService.Domain.ProductAggregate
{
    public class ParentProduct : EntityBase
    {
        public Guid CategoryId { get; private set; }

        [Required]
        [MaxLength(500)]
        public string Name { get; private set; }

        [MaxLength(500)]
        public string? Description { get; private set; }

        private readonly List<Product> _products;
        public IReadOnlyCollection<Product> Products => _products;

        protected ParentProduct()
        {
            _products = [];
        }

        public ParentProduct(Guid categoryId, string name, string? description)
        {
            CategoryId = categoryId;
            Name = name;
            Description = description;
        }

        #region Methods

        public void Update(Guid categoryId, string name, string? description)
        {
            CategoryId = categoryId;
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

        public void ChangeCategory(Guid categoryId)
        {
            CategoryId = categoryId;
        }

        public void AddProducts(List<Product> products)
        {
            _products.AddRange(products);
        }

        public void RemoveProducts(List<Product> products)
        {
            foreach (var product in products)
            {
                var existingProduct = _products.FirstOrDefault(x => x.Id == product.Id);

                if (existingProduct != null)
                    _products.Remove(existingProduct);
            }
        }

        public void ClearProducts()
        {
            _products.Clear();
        }

        #endregion Methods
    }
}
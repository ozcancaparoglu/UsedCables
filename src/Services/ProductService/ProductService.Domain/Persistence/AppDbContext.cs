using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ProductService.Domain.ProductAggregate;
using System.Reflection;
using UsedCables.Infrastructure.Entities;
using UsedCables.Infrastructure.Enums;

namespace ProductService.Domain.Persistence
{
    public class AppDbContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AppDbContext(DbContextOptions<AppDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public DbSet<ParentProduct> ParentProducts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductAttributes> ProductAttributes { get; set; }
        public DbSet<ProductPictures> ProductPictures { get; set; }
        public DbSet<ProductSellers> ProductSellers { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            int currentUser = int.TryParse(_httpContextAccessor.HttpContext.Items["User"].ToString(), out currentUser) ? currentUser : 1;

            foreach (var entry in ChangeTracker.Entries<EntityBase>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = DateTime.Now;
                        entry.Entity.State = (int)State.Active;
                        entry.Entity.ProcessedBy = currentUser;
                        break;

                    case EntityState.Modified:
                        entry.Entity.UpdatedAt = DateTime.Now;
                        entry.Entity.ProcessedBy = currentUser;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
                .Where(type => !string.IsNullOrEmpty(type.Namespace))
                .Where(type => type.BaseType != null && type.BaseType.IsGenericType
                && type.BaseType.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>));
            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.ApplyConfiguration(configurationInstance);
            }
            base.OnModelCreating(modelBuilder);
        }
    }
}
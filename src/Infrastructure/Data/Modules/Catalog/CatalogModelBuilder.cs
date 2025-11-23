using Microsoft.EntityFrameworkCore;
using TempooERP.Modules.Catalog.Domain.Products;

namespace TempooERP.Infrastructure.Data.Modules.Catalog;

public class CatalogModelBuilder : IModuleModelBuilder
{
    public void Configure(ModelBuilder modelBuilder)
    {
        var product = modelBuilder.Entity<Product>();

        product.ToTable("Products", "catalog");

        product.HasKey(p => p.Id);

        product.Property(p => p.Name)
               .IsRequired()
               .HasMaxLength(200);

        product.Property(p => p.Price)
               .IsRequired()
               .HasColumnType("decimal(18,2)");

        product.Property(p => p.TaxRate)
               .IsRequired();

        product.Property(p => p.IsActive)
               .IsRequired();

        product.Property(p => p.CreatedAt).IsRequired();
        product.Property(p => p.CreatedBy).HasMaxLength(100);
        product.Property(p => p.LastModifiedBy).HasMaxLength(100);

        product.HasIndex(p => p.Name);
        product.HasIndex(p => new { p.IsActive, p.Price });
    }
}

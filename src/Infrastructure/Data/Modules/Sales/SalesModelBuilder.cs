using Microsoft.EntityFrameworkCore;
using TempooERP.Modules.Sales.Domain.Orders;

namespace TempooERP.Infrastructure.Data.Modules.Sales;

public sealed class SalesModelBuilder : IModuleModelBuilder
{
    public void Configure(ModelBuilder modelBuilder)
    {
        var order = modelBuilder.Entity<Order>();
        order.ToTable("Orders", "sales");
        order.HasKey(o => o.Id);
        order.Property(o => o.Number).IsRequired();
        order.HasIndex(o => o.Number).IsUnique();
        order.Property(o => o.Status).IsRequired();
        order.Property(o => o.UserId).IsRequired();
        order.Property(o => o.CreatedAt).IsRequired();
        order.Property(o => o.CreatedBy).HasMaxLength(100);
        order.Property(o => o.LastModifiedBy).HasMaxLength(100);

        var line = modelBuilder.Entity<OrderLine>();
        line.ToTable("OrderLines", "sales");
        line.HasKey(l => l.Id);
        line.Property(l => l.OrderId).IsRequired();
        line.Property(l => l.ProductId).IsRequired();
        line.Property(l => l.ProductName).IsRequired().HasMaxLength(200);
        line.Property(l => l.UnitPrice).IsRequired().HasColumnType("decimal(18,2)");
        line.Property(l => l.Quantity).IsRequired();
        line.Ignore(l => l.TotalLinePrice);

        order.HasMany(o => o.OrderLines)
             .WithOne(l => l.Order)
             .HasForeignKey(l => l.OrderId)
             .OnDelete(DeleteBehavior.Cascade);

        line.HasIndex(l => new { l.OrderId, l.ProductId });
    }
}

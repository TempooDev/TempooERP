using System.ComponentModel.DataAnnotations;

namespace TempooERP.Modules.Catalog.Domain.Products;

public class Product
{
    public Guid Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string Name { get; private set; } = string.Empty;

    [Range(0, double.MaxValue)]
    public decimal Price { get; private set; }

    [Range(0, 100)]
    public int TaxRate { get; private set; }

    public bool IsActive { get; private set; }

    private Product(string name, decimal price, int taxRate, bool isActive)
    {
        Name = name;
        Price = price;
        TaxRate = taxRate;
        IsActive = isActive;
    }

    // Parameterless constructor for EF Core
    public Product() { }

    public static Product CreateProduct(
        string name,
        decimal price,
        int taxRate,
        bool isActive) => new(name, price, taxRate, isActive);

    public static Product UpdateDetails(
        Product product,
        string name,
        decimal price,
        bool isActive)
    {
        product.Name = name;
        product.Price = price;
        product.IsActive = isActive;
        return product;
    }
}

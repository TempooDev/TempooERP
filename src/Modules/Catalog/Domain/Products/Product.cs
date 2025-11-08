using System.ComponentModel.DataAnnotations;

namespace TempooERP.Modules.Catalog.Domain;

public class Product
{
    public Guid Id { get; set; }

    [Required]
    [MaxLength(200)]
    public required string Name { get; set; }

    [Range(0, double.MaxValue)]
    public decimal Price { get; set; }

    [Range(0, 100)]
    public int TaxRate { get; set; }

    public bool IsActive { get; set; }
}

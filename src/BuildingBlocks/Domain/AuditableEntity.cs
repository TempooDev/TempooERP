using TempooERP.BuildingBlocks.Application.Abstractions;

namespace TempooERP.BuildingBlocks.Domain;

public class AuditableEntity : EntityBase, IAuditable
{
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; } = null!;
    public DateTime? LastModifiedAt { get; set; }
    public string? LastModifiedBy { get; set; }
}

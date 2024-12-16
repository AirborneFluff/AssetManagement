namespace API.Domain.Shared;

public class BaseEntity : AuditEntity
{
    public string? TenantId { get; set; }
}
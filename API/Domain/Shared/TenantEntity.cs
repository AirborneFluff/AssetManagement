namespace API.Domain.Shared;

public class TenantEntity : AuditEntity
{
    public string? TenantId { get; set; }
}
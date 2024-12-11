namespace API.Domain.Tenant.DTOs;

public class TenantDto
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public required int Licences { get; set; }
}
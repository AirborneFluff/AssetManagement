using System.ComponentModel.DataAnnotations;

namespace API.Domain.Tenant.DTOs;

public class NewTenantDto
{
    [MaxLength(128)]
    public required string Name { get; set; }

    [Range(1, int.MaxValue)]
    public int Licences { get; set; }
}
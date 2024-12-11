using System.ComponentModel.DataAnnotations;
using API.Domain.Authentication;
using API.Domain.Shared;

namespace API.Domain.Tenant;

public class AppTenant : BaseEntity
{
    [MaxLength(128)]
    public required string Name { get; set; }

    [Range(1, int.MaxValue)]
    public int Licences { get; set; }

    public IList<AppUser> Users { get; set; } = [];
}
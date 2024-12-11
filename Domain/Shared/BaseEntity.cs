using System.ComponentModel.DataAnnotations;

namespace API.Domain.Shared;

public class BaseEntity
{
    [MaxLength(36)]
    public required string Id { get; set; } = Guid.NewGuid().ToString();
}
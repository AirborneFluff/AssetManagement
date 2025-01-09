using System.ComponentModel.DataAnnotations;

namespace API.Domain.Shared;

public class AuditEntity
{
    [MaxLength(36)]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    
    public DateTime CreatedOn { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public DateTime? DeletedOn { get; set; }
    public string? CreatedBy { get; set; }
    public string? ModifiedBy { get; set; }
    public string? DeletedBy { get; set; }
    public bool IsDeleted { get; set; } = false;
}
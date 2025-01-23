namespace API.Domain.Modules.Dtos;

public class ToggleModuleDto
{
    public required string TenantId { get; set; }
    public required string ModuleIdentifier { get; set; }
}
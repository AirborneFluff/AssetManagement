namespace API.Domain.Authentication.Dtos;

public class AppUserDto
{
    public required string Id { get; set; }
    public required string Email { get; set; }
    public string? TenantId { get; set; }
    public string? Role { get; set; }
}
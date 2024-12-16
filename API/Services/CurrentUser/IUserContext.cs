namespace API.Services.CurrentUser;

public interface IUserContext
{
    string UserId { get; }
    string? TenantId { get; }
    string Email { get; }
}
namespace API.Services.CurrentUser;

public interface ICurrentUserService
{
    string UserId { get; }
    string TenantId { get; }
    string Email { get; }
}
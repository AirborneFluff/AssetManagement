using System.Reflection;

namespace API.Domain.Authentication.Constants;

public static class AppUserRoles
{
    public const string SuperUser = "SuperUser";
    public const string Admin = "Admin";
    public const string Client = "Client";
    
    public static List<string> GetRoles()
    {
        return typeof(AppUserRoles)
            .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
            .Where(field => field is { IsLiteral: true, IsInitOnly: false } && field.FieldType == typeof(string))
            .Select(field => field.GetRawConstantValue()?.ToString())
            .ToList()!;
    }
}
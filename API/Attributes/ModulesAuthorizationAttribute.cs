namespace API.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
public class ModuleAuthorizationAttribute(string moduleIdentity) : Attribute
{
    public string ModuleIdentity { get; } = moduleIdentity;
}
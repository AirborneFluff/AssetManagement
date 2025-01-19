namespace API.Domain.Modules;

public class ModuleSetting(string displayName, string identifier, bool automaticallyEnabled)
{
    public string DisplayName { get; } = displayName;
    public string Identifier { get; } = identifier;
    public bool AutomaticallyEnabled { get; } = automaticallyEnabled;
}
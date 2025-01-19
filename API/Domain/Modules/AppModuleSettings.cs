namespace API.Domain.Modules;

public static class AppModuleSettings
{
    private static readonly ModuleSetting AssetManagement = new
    (
        displayName: "Asset Management",
        identifier: "ASSET_MANAGEMENT",
        automaticallyEnabled: true
    );

    private static readonly ModuleSetting StockOrdersManagement = new
    (
        displayName: "Stock Orders Management",
        identifier: "STOCK_ORDERS_MANAGEMENT",
        automaticallyEnabled: false
    );
    
    public static List<ModuleSetting> GetAllSettings()
    {
        return
        [
            AssetManagement,
            StockOrdersManagement
        ];
    }
}
namespace API.Domain.Modules;

public static class AppModuleSettings
{
    private static readonly ModuleSetting AssetManagement = new
    (
        displayName: "Asset Management",
        identifier: AppModules.AssetManagement,
        automaticallyEnabled: true
    );

    private static readonly ModuleSetting PurchaseOrdersManagement = new
    (
        displayName: "Purchase Orders Management",
        identifier: AppModules.PurchaseOrders,
        automaticallyEnabled: false
    );

    private static readonly ModuleSetting SalesOrdersManagement = new
    (
        displayName: "Sales Orders Management",
        identifier: AppModules.SalesOrders,
        automaticallyEnabled: false
    );
    
    public static List<ModuleSetting> GetAllSettings()
    {
        return
        [
            AssetManagement,
            PurchaseOrdersManagement,
            SalesOrdersManagement
        ];
    }
}
using API.Domain.Shared;

namespace API.Domain.PurchaseOrders;

public class PurchaseOrder : TenantEntity
{
    public required string Reference { get; set; }
}
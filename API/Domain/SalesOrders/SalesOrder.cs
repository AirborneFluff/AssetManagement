using API.Domain.Shared;

namespace API.Domain.SalesOrders;

public class SalesOrder : TenantEntity
{
    public required string Reference { get; set; }
}
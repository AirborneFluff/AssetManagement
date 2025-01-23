using API.Attributes;
using API.Domain.Modules;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.PurchaseOrders;

[Authorize]
[ModuleAuthorization(AppModules.PurchaseOrders)]
public partial class PurchaseOrdersController(IMediator mediator) : BaseApiController
{
    [HttpPost]
    public async Task<ActionResult> Test()
    {
        return Ok();
    }
}
using API.Attributes;
using API.Domain.Modules;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers.SalesOrders;

[Authorize]
[ModuleAuthorization(AppModules.SalesOrders)]
public partial class SalesOrdersController(IMediator mediator) : BaseApiController
{
    
}
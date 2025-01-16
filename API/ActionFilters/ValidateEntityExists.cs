using API.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using API.Domain.Shared;
using FluentResults;

namespace API.ActionFilters;

public class ValidateEntityExistsAttribute<T> : TypeFilterAttribute where T : BaseEntity
{
    public ValidateEntityExistsAttribute(string idName = "id")
        : base(typeof(ValidateEntityExists<T>))
    {
        Arguments = [idName];
    }
}

public class ValidateEntityExists<T>(IUnitOfWork unitOfWork, string idName) : IAsyncActionFilter where T : BaseEntity
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (context.ActionArguments[idName] is not string entityId) 
        {
            throw new Exception($"'{idName}' not provided for validation");
        }
        
        var entityExists = await unitOfWork.Context.Set<T>()
            .AnyAsync(e => e.Id == entityId);
        
        if (!entityExists)
        {
            context.Result = new BadRequestObjectResult(Result.Fail($"{typeof(T).Name} not found.").Errors);
            return;
        }
    
        await next.Invoke();
    }
}
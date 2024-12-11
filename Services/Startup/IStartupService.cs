using FluentResults;

namespace API.Services.Startup;

public interface IStartupService
{
    Task<Result> Execute();
}
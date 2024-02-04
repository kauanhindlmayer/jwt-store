using JwtStore.Core.Contexts.AccountContext.UseCases.Create;
using MediatR;

namespace JwtStore.Api.Extensions;

public static class AccountContextExtension
{
    public static void AddAccountContext(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<
            JwtStore.Core.Contexts.AccountContext.UseCases.Create.Contracts.IRepository,
            JwtStore.Infra.Contexts.AccountContext.UseCases.Create.Repository
        >();

        builder.Services.AddTransient<
            JwtStore.Core.Contexts.AccountContext.UseCases.Create.Contracts.IService,
            JwtStore.Infra.Contexts.AccountContext.UseCases.Create.Service
        >();
    }

    public static void MapAccountEndpoints(this WebApplication app)
    {
        app.MapPost("api/v1/users", async (Request request, IRequestHandler<Request, Response> handler) =>
        {
            var result = await handler.Handle(request, default);
            return result.IsSuccess
                ? Results.Created("api/v1/users", result)
                : Results.Json(result, statusCode: result.StatusCode);
        });
    }
}
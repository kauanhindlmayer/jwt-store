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

        builder.Services.AddTransient<
            JwtStore.Core.Contexts.AccountContext.UseCases.Authenticate.Contracts.IRepository,
            JwtStore.Infra.Contexts.AccountContext.UseCases.Authenticate.Repository
        >();
    }

    public static void MapAccountEndpoints(this WebApplication app)
    {
        app.MapPost("api/v1/users", async (JwtStore.Core.Contexts.AccountContext.UseCases.Create.Request request,
            IRequestHandler<JwtStore.Core.Contexts.AccountContext.UseCases.Create.Request,
                JwtStore.Core.Contexts.AccountContext.UseCases.Create.Response> handler) =>
        {
            var result = await handler.Handle(request, default);
            return result.IsSuccess
                ? Results.Created("api/v1/users", result)
                : Results.Json(result, statusCode: result.StatusCode);
        });

        app.MapPost("api/v1/authenticate", async (
            JwtStore.Core.Contexts.AccountContext.UseCases.Authenticate.Request request,
            IRequestHandler<JwtStore.Core.Contexts.AccountContext.UseCases.Authenticate.Request,
                JwtStore.Core.Contexts.AccountContext.UseCases.Authenticate.Response> handler) =>
        {
            var result = await handler.Handle(request, default);
            
            if (!result.IsSuccess)
                return Results.Json(result, statusCode: result.StatusCode);
            
            if (result.Data is null)
                return Results.Json(result, statusCode: 500);
            
            result.Data.Token = JwtExtension.Generate(result.Data);
            return Results.Ok(result);
        });
    }
}
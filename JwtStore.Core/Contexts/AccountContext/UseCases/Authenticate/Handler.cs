using JwtStore.Core.Contexts.AccountContext.Entities;
using JwtStore.Core.Contexts.AccountContext.UseCases.Authenticate.Contracts;
using MediatR;

namespace JwtStore.Core.Contexts.AccountContext.UseCases.Authenticate;

public class Handler : IRequestHandler<Request, Response>
{
    private readonly IRepository _repository;

    public Handler(IRepository repository) => _repository = repository;

    public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
    {
        try
        {
            var res = Specification.Ensure(request);
            if (!res.IsValid)
                return new Response("Invalid Request", 400, res.Notifications);
        }
        catch
        {
            return new Response("Invalid Request", 500);
        }
        
        User? user;
        try
        {
            user = await _repository.GetUserByEmailAsync(request.Email, cancellationToken);
            if (user is null)
                return new Response("Account not found", 404);
        }
        catch (Exception e)
        {
            return new Response("Error on get user", 500);
        }
        
        if (!user.Password.Challenge(request.Password))
            return new Response("User or password invalid", 400);

        try
        {
            if (!user.Email.Verification.IsActive)
                return new Response("Email not verified", 400);
        }
        catch
        {
            return new Response("Error on check email verification", 500);
        }
        
        try
        {
            var data = new ResponseData
            {
                Id = user.Id.ToString(),
                Name = user.Name,
                Email = user.Email,
                Roles = user.Roles.Select(x => x.Name).ToArray()
            };

            return new Response(string.Empty, data);
        }
        catch
        {
            return new Response("Error on getting user data", 500);
        }
    }
}
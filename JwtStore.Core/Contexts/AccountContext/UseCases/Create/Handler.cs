using JwtStore.Core.Contexts.AccountContext.Entities;
using JwtStore.Core.Contexts.AccountContext.UseCases.Create.Contracts;
using JwtStore.Core.Contexts.AccountContext.ValueObjects;
using MediatR;

namespace JwtStore.Core.Contexts.AccountContext.UseCases.Create;

public class Handler : IRequestHandler<Request, Response>
{
    private readonly IRepository _repository;
    private readonly IService _service;
    
    public Handler(IRepository repository, IService service)
    {
        _repository = repository;
        _service = service;
    }

    public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
    {
        try
        {
            var result = Specification.Ensure(request);
            if (!result.IsValid)
                return new Response("Request is invalid", 400, result.Notifications);
        }
        catch
        {
            return new Response("Request is invalid", 500, null);
        }

        Email email;
        Password password;
        User user;
        
        try
        {
            email = new Email(request.Email);
            password = new Password(request.Password);
            user = new User(request.Name, email, password);
        }
        catch (Exception ex)
        {
            return new Response(ex.Message, 400);
        }
        
        try
        {
            var userAlreadyExists = await _repository.AnyAsync(request.Email, cancellationToken);
            if (userAlreadyExists)
                return new Response("User already exists", 400);
        }
        catch (Exception)
        {
            return new Response("Error on check user", 500);
        }
        
        try
        {
            await _repository.SaveAsync(user, cancellationToken);
        }
        catch (Exception)
        {
            return new Response("Error on create user", 500);
        }

        try
        {
            await _service.SendVerificationEmailAsync(user, cancellationToken);
        }
        catch (Exception)
        {
            // ignored
        }

        return new Response("User created", new ResponseData(user.Id, user.Name, user.Email));
    }
}
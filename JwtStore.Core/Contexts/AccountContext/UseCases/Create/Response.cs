using Flunt.Notifications;

namespace JwtStore.Core.Contexts.AccountContext.UseCases.Create;

public class Response: SharedContext.UseCases.Response
{
    protected Response()
    {
    }
    
    public Response(string message, int statusCode, IEnumerable<Notification>? notifications)
    {
        Message = message;
        StatusCode = statusCode;
        Notifications = notifications;
    }
    
    public Response(string message, ResponseData data)
    {
        Message = message;
        StatusCode = 201;
        Notifications = null;
        Data = data;
    }
    
    public ResponseData? Data { get; set; }
}

public record ResponseData(Guid Id, string Name, string Email);

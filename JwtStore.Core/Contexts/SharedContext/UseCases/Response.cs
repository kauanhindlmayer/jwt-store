using Flunt.Notifications;

namespace JwtStore.Core.Contexts.SharedContext.UseCases;

public abstract class Response
{
    public string Message { get; set; } = string.Empty;
    public int StatusCode { get; set; } = 400;
    public bool IsSuccess => StatusCode is >= 200 and <= 299;
    public IEnumerable<Notification>? Notifications { get; set; }
}
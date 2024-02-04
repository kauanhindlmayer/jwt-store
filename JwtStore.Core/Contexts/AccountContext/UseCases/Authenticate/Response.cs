﻿using Flunt.Notifications;

namespace JwtStore.Core.Contexts.AccountContext.UseCases.Authenticate;

public class Response: SharedContext.UseCases.Response
{
    protected Response()
    {
    }
    
    public Response(string message, int statusCode, IEnumerable<Notification>? notifications = null)
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

public class ResponseData
{
    public string Token { get; set; } = string.Empty;
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string[] Roles { get; set; } = Array.Empty<string>();
}

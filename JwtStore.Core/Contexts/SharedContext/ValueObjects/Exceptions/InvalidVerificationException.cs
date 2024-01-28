namespace JwtStore.Core.Contexts.SharedContext.ValueObjects.Exceptions;

public class InvalidVerificationException : Exception
{
    private const string DefaultErrorMessage = "Verification code is invalid";
    
    public InvalidVerificationException(string message = DefaultErrorMessage) : base(message)
    {
    }
}
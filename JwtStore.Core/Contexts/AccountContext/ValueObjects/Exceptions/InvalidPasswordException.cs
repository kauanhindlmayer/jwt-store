namespace JwtStore.Core.Contexts.AccountContext.ValueObjects.Exceptions;

public class InvalidPasswordException : Exception
{
    private const string DefaultErrorMessage = "Password is invalid";

    private InvalidPasswordException(string message = DefaultErrorMessage) : base(message)
    {
    }
}
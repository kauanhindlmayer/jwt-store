using System.Text.RegularExpressions;

namespace JwtStore.Core.Contexts.AccountContext.ValueObjects.Exceptions;

public class InvalidEmailException : Exception
{
    private const string EmailRegexPattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
    private const string DefaultErrorMessage = "E-mail is invalid";

    private InvalidEmailException(string message = DefaultErrorMessage) : base(message)
    {
    }

    public static void ThrowIfInvalid(string? address, string message = DefaultErrorMessage)
    {
        if (string.IsNullOrEmpty(address))
        {
            throw new InvalidEmailException(message);
        }

        if (!Regex.IsMatch(address, EmailRegexPattern))
        {
            throw new InvalidEmailException(message);
        }
    }
}
using Flunt.Notifications;
using Flunt.Validations;

namespace JwtStore.Core.Contexts.AccountContext.UseCases.Create;

public static class Specification
{
    public static Contract<Notification> Ensure(Request request)
        => new Contract<Notification>()
            .Requires()
            .IsLowerThan(request.Name.Length, 160, "Name", "Name should be less than 160 characters")
            .IsGreaterThan(request.Name.Length, 3, "Name", "Name should be more than 3 characters")
            .IsLowerThan(request.Password.Length, 40, "Password", "Password should be less than 40 characters")
            .IsGreaterThan(request.Password.Length, 8, "Password", "Password should be more than 8 characters")
            .IsEmail(request.Email, "Email", "Email is invalid");
}
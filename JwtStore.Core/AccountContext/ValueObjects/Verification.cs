using JwtStore.Core.SharedContext.ValueObjects;
using JwtStore.Core.SharedContext.ValueObjects.Exceptions;

namespace JwtStore.Core.AccountContext.ValueObjects;

public class Verification : ValueObject
{
    public Verification()
    {
    }

    public string Code { get; } = Guid.NewGuid().ToString("N")[..6].ToUpper();
    public DateTime? ExpiresAt { get; private set; } = DateTime.UtcNow.AddMinutes(5);
    public DateTime? VerifiedAt { get; private set; } = null;
    public bool IsActive => VerifiedAt.HasValue && !ExpiresAt.HasValue;

    public void Verify(string code)
    {
        if (!IsActive)
            throw new InvalidVerificationException("This code is no longer valid!");

        if (ExpiresAt < DateTime.UtcNow)
            throw new InvalidVerificationException("This code has expired!");

        if (!string.Equals(code.Trim(), Code.Trim(), StringComparison.InvariantCultureIgnoreCase))
            throw new InvalidVerificationException("This code is invalid!");

        ExpiresAt = null;
        VerifiedAt = DateTime.UtcNow;
    }
}
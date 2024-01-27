﻿using JwtStore.Core.AccountContext.ValueObjects.Exceptions;
using JwtStore.Core.SharedContext.Extensions;
using JwtStore.Core.SharedContext.ValueObjects;

namespace JwtStore.Core.AccountContext.ValueObjects;

public class Email : ValueObject
{
    protected Email()
    {
    }
    
    public Email(string address)
    {
        Address = address;
        InvalidEmailException.ThrowIfInvalid(address);
    }

    public string Address { get; } = string.Empty;
    
    public string Hash => Address.ToBase64();
    
    public Verification Verification { get; private set; } = new();
    
    public void ResetVerification() => Verification = new Verification();

    public static implicit operator string(Email email) => email.ToString();
    
    public static implicit operator Email(string address) => new(address);
    
    public override string ToString() => Address;
}
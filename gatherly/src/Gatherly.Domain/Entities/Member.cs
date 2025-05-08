using Gatherly.Domain.Primitives;
using Gatherly.Domain.ValueObjects;

namespace Gatherly.Domain.Entities;

public sealed class Member
    : Entity
{
    private Member(Guid id, Email email, FirstName firstName, LastName lastName) : base(id)
    {
        Email = email;
        FirstName = firstName;
        LastName = lastName;
    }
    
    public Email Email { get; set; }

    public FirstName FirstName { get; set; }

    public LastName LastName { get; set; }

    public static Member Create(
        Guid id,
        Email email,
        FirstName firstName,
        LastName lastName)
    {
        var member = new Member(
            id,
            email,
            firstName,
            lastName);
        
        return member;
    }
}
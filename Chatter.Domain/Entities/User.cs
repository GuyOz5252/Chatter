using SharedKernel.Interfaces;
using SharedKernel.Results;

namespace Chatter.Domain.Entities;

public class User : IAggregateRoot
{
    public Guid Id { get; init; } = Guid.NewGuid();
    
    public string UserName { get; private set; }
    
    public string Email { get; private set; }

    public User(string userName, string email)
    {
        UserName = userName;
        Email = email;
    }
}
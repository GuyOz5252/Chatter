namespace Chatter.Client.Models;

public class User
{
    public required Guid Id { get; init; }
    
    public required string UserName { get; init; }
}
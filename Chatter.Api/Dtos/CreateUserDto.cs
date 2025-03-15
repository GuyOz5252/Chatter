namespace Chatter.Api.Dtos;

public record CreateUserDto 
{
    public required string Username { get; init; }
    
    public required string Email { get; init; }
}
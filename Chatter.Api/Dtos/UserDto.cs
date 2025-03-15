namespace Chatter.Api.Dtos;

public record UserDto
{
    public required Guid Id { get; set; }
    
    public required string UserName { get; set; }
    
    public required string Email { get; set; }
}
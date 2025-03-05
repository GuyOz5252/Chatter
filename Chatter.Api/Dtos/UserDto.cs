namespace Chatter.Api.Dtos;

public class UserDto
{
    public required  Guid Id { get; set; }
    
    public required  string Username { get; set; }
    
    public required  string Email { get; set; }
}
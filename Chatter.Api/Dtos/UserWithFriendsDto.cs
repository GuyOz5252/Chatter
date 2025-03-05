namespace Chatter.Api.Dtos;

public class UserWithFriendsDto
{
    public required Guid Id { get; set; }
    
    public required string UserName { get; set; }
    
    public required string Email { get; set; }
    
    public required List<UserDto> Friends { get; set; }
}
namespace Chatter.Api.Dtos;

public record FullUserDto : UserDto
{
    public required List<UserDto> Friends { get; set; }
}
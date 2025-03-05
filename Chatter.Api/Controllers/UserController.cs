using Chatter.Api.Dtos;
using Chatter.Api.Extensions;
using Chatter.Application.Users.Commands.AddFriend;
using Chatter.Application.Users.Commands.CreateUser;
using Chatter.Application.Users.Queries.GetUserById;
using Chatter.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Chatter.Api.Controllers;

[ApiController]
[Route("users")]
public class UserController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;
    
    [HttpGet("{userId:guid}")]
    public async Task<IActionResult> GetById(Guid userId)
    {
        var userResult = await _mediator.Send(new GetUserByIdQuery(userId));
        return userResult.Match<IActionResult>(value =>
        {
            var userWithFriendsDto = new UserWithFriendsDto
            {
                Id = value.Id,
                UserName = value.UserName,
                Email = value.Email,
                Friends = value.Friendships.Select(userFriendship => new UserDto
                {
                    Id = userFriendship.Friend.Id,
                    Username = userFriendship.Friend.UserName,
                    Email = userFriendship.Friend.Email
                }).ToList()
            };
            return Ok(userWithFriendsDto);
        }, error => error.ToProblemDetails());
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDto createUserDto)
    {
        var result = await _mediator.Send(new CreateUserCommand(createUserDto.Username, createUserDto.Email));
        return result.Match<IActionResult>(
            value => Ok(value),
            error => error.ToProblemDetails());
    }

    [HttpPost("{userId:guid}")]
    public async Task<IActionResult> AddFriend(Guid userId, [FromQuery] Guid friendId)
    {
        var result = await _mediator.Send(new AddFriendCommand(userId, friendId));
        return result.Match<IActionResult>(Ok, error => error.ToProblemDetails());
    }
}

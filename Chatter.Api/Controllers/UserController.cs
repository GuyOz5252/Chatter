using Chatter.Api.Dtos;
using Chatter.Api.Extensions;
using Chatter.Application.Users.Commands.AddFriend;
using Chatter.Application.Users.Commands.CreateUser;
using Chatter.Application.Users.Queries.GetUserById;
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
        return userResult.Match<IActionResult>(Ok, error => error.ToProblemDetails());
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] UserDto userDto)
    {
        var result = await _mediator.Send(new CreateUserCommand(userDto.Username, userDto.Password));
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

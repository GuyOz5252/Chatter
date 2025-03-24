using Chatter.Api.Dtos;
using Chatter.Api.Extensions;
using Chatter.Application.Users.Commands.CreateUser;
using Chatter.Application.Users.Queries.GetUserById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Chatter.Api.Controllers;

[ApiController]
[Route("users")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet("{userId:guid}")]
    public async Task<IActionResult> GetById(Guid userId)
    {
        var userResult = await _mediator.Send(new GetUserByIdQuery(userId));
        return userResult.Match<IActionResult>(value =>
        {
            var userDto = new UserDto
            {
                Id = value.Id,
                UserName = value.UserName,
                Email = value.Email
            };
            return Ok(userDto);
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
}

using Chatter.Application.Users.Queries.GetUserById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Chatter.Api.Controllers;

[ApiController]
public class UserController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;
    
    [HttpGet("users/{userId:guid}")]
    public async Task<IActionResult> GetById(Guid userId)
    {
        var userResult = await _mediator.Send(new GetUserByIdQuery(userId));
        return userResult.Match<IActionResult>(Ok, BadRequest);
    }
}

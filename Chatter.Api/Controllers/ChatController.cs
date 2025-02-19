using Chatter.Api.Dtos;
using Chatter.Api.Extensions;
using Chatter.Application.Chats.Commands.SendChatMessage;
using Chatter.Application.Chats.Queries.GetChatsByUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Chatter.Api.Controllers;

[ApiController]
[Route("chats")]
public class ChatController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost("{chatId:Guid}/send-message")]
    public async Task<IActionResult> SendMessage([FromRoute] Guid chatId, [FromBody] ChatMessageDto chatMessageDto)
    {
        var result =
            await _mediator.Send(new SendChatMessageCommand(chatId, chatMessageDto.UserId, chatMessageDto.ChatMessageContent));
        return result.Match<IActionResult>(Ok, error => error.ToProblemDetails());
    }

    [HttpGet]
    public async Task<IActionResult> GetChatsByUser([FromQuery] Guid userId)
    {
        var result = await _mediator.Send(new GetChatsByUserQuery(userId));
        return result.Match<IActionResult>(Ok, error => error.ToProblemDetails());
    }
}

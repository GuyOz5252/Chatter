using Chatter.Api.Dtos;
using Chatter.Api.Extensions;
using Chatter.Application.Chat.Commands.SendChatMessage;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Chatter.Api.Controllers;

[ApiController]
public class ChatController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost("chats/{chatId:Guid}/send-message")]
    public async Task<IActionResult> SendMessage([FromRoute] Guid chatId, [FromBody] ChatMessageDto chatMessageDto)
    {
        var result =
            await _mediator.Send(new SendChatMessageCommand(chatId, chatMessageDto.UserId, chatMessageDto.ChatMessageContent));
        return result.Match<IActionResult>(Ok, error => error.ToProblemDetails());
    }
}
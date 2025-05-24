using Chatter.Domain.Entities;
using SharedKernel.Messaging;

namespace Chatter.Application.Chats.Get;

public record GetChatsQuery : IQuery<List<Chat>>
{
    public required Guid UserId { get; init; }
}

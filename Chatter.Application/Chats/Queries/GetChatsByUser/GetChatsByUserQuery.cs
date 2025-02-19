using Chatter.Domain.Entities;
using SharedKernel.Interfaces;

namespace Chatter.Application.Chats.Queries.GetChatsByUser;

public record GetChatsByUserQuery(Guid UserId) : IQuery<List<Chat>>;
using Chatter.Domain.Abstract;
using Chatter.Domain.Entities;
using SharedKernel.Messaging;
using SharedKernel.Results;

namespace Chatter.Application.Chats.Get;

public class GetChatsQueryHandler : IQueryHandler<GetChatsQuery, List<Chat>>
{
    private readonly IChatRepository _chatRepository;

    public GetChatsQueryHandler(IChatRepository chatRepository)
    {
        _chatRepository = chatRepository;
    }

    public async Task<Result<List<Chat>>> HandleAsync(GetChatsQuery query, CancellationToken cancellationToken = default)
    {
        return await _chatRepository.GetChatsByUserAsync(query.UserId, cancellationToken);
    }
}

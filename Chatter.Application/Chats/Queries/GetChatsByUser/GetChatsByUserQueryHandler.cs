using Chatter.Domain.Entities;
using Chatter.Domain.Interfaces;
using Chatter.Domain.Specifications;
using SharedKernel.Interfaces;
using SharedKernel.Results;

namespace Chatter.Application.Chats.Queries.GetChatsByUser;

public class GetChatsByUserQueryHandler : IQueryHandler<GetChatsByUserQuery, List<Chat>>
{
    private IChatRepository _chatRepository;

    public GetChatsByUserQueryHandler(IChatRepository chatRepository)
    {
        _chatRepository = chatRepository;
    }

    public async Task<Result<List<Chat>>> HandleAsync(GetChatsByUserQuery query, CancellationToken token = default)
    {
        return await _chatRepository.ListAsync(new ChatsByUserSpecification(query.UserId), token);
    }
}
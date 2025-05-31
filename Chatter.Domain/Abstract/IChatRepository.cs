using Chatter.Domain.Entities;
using SharedKernel.Results;

namespace Chatter.Domain.Abstract;

public interface IChatRepository
{
    Task<Result<Chat>> GetAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Result<Chat>> GetFullAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Result<List<Chat>>> GetChatsByUserAsync(Guid userId, CancellationToken cancellationToken = default);
    void Create(Chat chat);
    void Update(Chat chat);
}

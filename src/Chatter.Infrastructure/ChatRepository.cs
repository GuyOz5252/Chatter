using Chatter.Domain.Abstract;
using Chatter.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Results;

namespace Chatter.Infrastructure;

public class ChatRepository : IChatRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ChatRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<Chat>> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Chats.FindAsync([id], cancellationToken);
    }

    public async Task<Result<List<Chat>>> GetChatsByUserAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Chats
            .Where(chat => chat.ParticipantsUserIds.Contains(userId))
            .Include(chat => chat.ParticipantsUserIds)
            .Include(chat => chat.ChatMessages)
            .ToListAsync(cancellationToken);
    }

    public void Create(Chat chat)
    {
        _dbContext.Chats.Add(chat);
    }
}

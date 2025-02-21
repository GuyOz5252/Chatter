using Chatter.Domain.Entities;
using Chatter.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Chatter.Infrastructure;

public class ChatRepository : EntityFrameworkRepositoryBase<Chat>, IChatRepository
{
    public ChatRepository(DbContext dbContext) : base(dbContext)
    {
    }
}
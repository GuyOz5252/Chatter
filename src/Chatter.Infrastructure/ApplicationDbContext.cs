using Chatter.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Chatter.Infrastructure;

public class ApplicationDbContext : DbContext
{
    public DbSet<User> Users { get; init; }
    
    public DbSet<Chat> Chats { get; init; }
}

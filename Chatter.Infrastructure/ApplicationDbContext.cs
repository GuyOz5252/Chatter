using Chatter.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Chatter.Infrastructure;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    
    public DbSet<User> Users { get; set; }
    
    public DbSet<Chat> Chats { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Chat>()
            .HasMany(c => c.Participants)
            .WithMany();
        
        modelBuilder.Entity<Chat>()
            .HasMany(chat => chat.ChatMessages)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ChatMessage>()
            .HasOne(chatMessage => chatMessage.Sender)
            .WithMany()
            .HasForeignKey("SenderId");
    }
}
using Chatter.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Chatter.Infrastructure;

public class ApplicationDbContext : DbContext
{
    public DbSet<User> Users { get; init; }
    public DbSet<Chat> Chats { get; init; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasKey(user => user.Id);
        modelBuilder.Entity<User>()
            .Property(user => user.UserName)
            .IsRequired();
        modelBuilder.Entity<User>()
            .Property(user => user.Email)
            .IsRequired();
        modelBuilder.Entity<User>()
            .Property(user => user.PasswordHash)
            .IsRequired();
        
        modelBuilder.Entity<Chat>()
            .HasKey(chat => chat.Id);
        // modelBuilder.Entity<Chat>().Metadata
        //     .FindNavigation(nameof(Chat.ChatMessages))!
        //     .SetPropertyAccessMode(PropertyAccessMode.Field);
        // modelBuilder.Entity<Chat>().Metadata
        //     .FindNavigation(nameof(Chat.ParticipantsUserIds))!
        //     .SetPropertyAccessMode(PropertyAccessMode.Field);
        modelBuilder.Entity<Chat>()
            .Property<List<Guid>>("_participantsUserIds")
            .HasConversion(
                guids => string.Join(",", guids),
                guids => guids.Split(",", StringSplitOptions.RemoveEmptyEntries).Select(Guid.Parse).ToList())
            .IsRequired();
        
        modelBuilder.Entity<ChatMessage>()
            .HasKey(chatMessage => chatMessage.Id);
        modelBuilder.Entity<ChatMessage>()
            .Property(chatMessage => chatMessage.SenderUserId)
            .IsRequired();
        modelBuilder.Entity<ChatMessage>()
            .Property(chatMessage => chatMessage.MessageContent)
            .IsRequired();
        modelBuilder.Entity<ChatMessage>()
            .Property(chatMessage => chatMessage.SentAt)
            .IsRequired();
        modelBuilder.Entity<ChatMessage>()
            .HasOne<Chat>()
            .WithMany(nameof(Chat.ChatMessages))
            .HasForeignKey(chat => chat.Id);
    }
}

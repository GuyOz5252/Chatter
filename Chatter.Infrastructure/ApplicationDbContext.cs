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
        modelBuilder.Entity<User>(builder =>
        {
            builder.HasKey(e => e.Id);
            builder.Property(user => user.UserName).IsRequired();
            builder.Property(user => user.Email).IsRequired();
            builder.Property(user => user.PasswordHash).IsRequired();
        });
        
        modelBuilder.Entity<Chat>(builder =>
        {
            builder.HasKey(chat => chat.Id);
            builder.HasMany(chat => chat.Participants)
                .WithOne()
                .HasForeignKey(chatParticipant => chatParticipant.ChatId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(chat => chat.ChatMessages)
                .WithOne()
                .HasForeignKey(chatMessage => chatMessage.ChatId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<ChatParticipant>(builder =>
        {
            builder.HasKey(chatParticipant => chatParticipant.Id);
            builder.Property(chatParticipant => chatParticipant.UserId).IsRequired();
            builder.Property(chatParticipant => chatParticipant.ChatId).IsRequired();
        });

        modelBuilder.Entity<ChatMessage>(builder =>
        {
            builder.HasKey(chatMessage => chatMessage.Id);
            builder.Property(chatMessage => chatMessage.SenderUserId).IsRequired();
            builder.Property(chatMessage => chatMessage.MessageContent).IsRequired();
            builder.Property(chatMessage => chatMessage.SentAt).IsRequired();
            // builder.HasOne(chatMessage => chatMessage.Chat)
            //     .WithMany(chat => chat.ChatMessages)
            //     .HasForeignKey(m => m.ChatId)
            //     .OnDelete(DeleteBehavior.Cascade);
        });
    }
}

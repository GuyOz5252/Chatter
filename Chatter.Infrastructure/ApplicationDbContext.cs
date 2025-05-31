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
            builder.HasMany<ChatMessage>()
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<ChatMessage>(builder =>
        {
            builder.HasKey(chatMessage => chatMessage.Id);
            builder.Property(chatMessage => chatMessage.SenderUserId).IsRequired();
            builder.Property(chatMessage => chatMessage.MessageContent).IsRequired();
            builder.Property(chatMessage => chatMessage.SentAt).IsRequired();
            builder
                .HasOne<Chat>()
                .WithMany(nameof(Chat.ChatMessages))
                .HasForeignKey(chatMessage => chatMessage.Id);
        });
    }
}

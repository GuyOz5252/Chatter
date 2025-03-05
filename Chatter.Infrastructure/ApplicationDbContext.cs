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
        
        modelBuilder.Entity<UserFriendship>()
            .HasKey(userFriendship => new { userFriendship.UserId, userFriendship.FriendId });

        modelBuilder.Entity<UserFriendship>()
            .HasOne(userFriendship => userFriendship.User)
            .WithMany(user => user.Friendships)
            .HasForeignKey(userFriendship => userFriendship.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<UserFriendship>()
            .HasOne(userFriendship => userFriendship.Friend)
            .WithMany()
            .HasForeignKey(userFriendship => userFriendship.FriendId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
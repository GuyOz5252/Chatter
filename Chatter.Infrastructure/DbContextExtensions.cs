using Chatter.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Chatter.Infrastructure;

public static class DbContextExtensions
{
    public static async Task Seed(this DbContext dbContext, CancellationToken cancellationToken = default)
    {
        await dbContext.Database.EnsureCreatedAsync(cancellationToken);
        await dbContext.Database.MigrateAsync(cancellationToken);
        
        dbContext.AddRange(
            new User("guy", "guy@gmail.com"),
            new User("guy1", "guy1@gmail.com"),
            new User("guy2", "guy2@gmail.com"),
            new User("guy3", "guy3@gmail.com"));
        
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
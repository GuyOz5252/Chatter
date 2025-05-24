using Chatter.Domain.Abstract;
using Chatter.Domain.Entities;
using SharedKernel.Results;

namespace Chatter.Infrastructure;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _dbContext;

    public UserRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<User>> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Users.FindAsync([id], cancellationToken);
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var user = await _dbContext.Users.FindAsync([id], cancellationToken);
        return user is not null;
    }
    
    public void Create(User user)
    {
        _dbContext.Users.Add(user);
    }
}

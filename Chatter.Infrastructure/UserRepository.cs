using Chatter.Domain.Entities;
using Chatter.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Interfaces;
using SharedKernel.Results;

namespace Chatter.Infrastructure;

public class UserRepository : EntityFrameworkRepositoryBase<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public override async Task<Result<User>> GetBySpecificationAsync(ISpecification<User> specification, CancellationToken cancellationToken = default)
    {
        return (await DbContext.Set<User>()
            .Where(specification.Query)
            .Include(user => user.Friendships)
            .FirstOrDefaultAsync(cancellationToken))!;
    }

    // public async Task<Result<User>> GetById(Guid userId)
    // {
    //     return (await DbContext.Set<User>().Where(user => user.UserId.Equals(userId)).FirstOrDefaultAsync())!;
    // }
}
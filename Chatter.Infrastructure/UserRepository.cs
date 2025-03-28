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
}
using Chatter.Domain.Entities;
using Chatter.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Chatter.Infrastructure;

public class UserRepository : EntityFrameworkRepositoryBase<User>, IUserRepository
{
    public UserRepository(DbContext dbContext) : base(dbContext)
    {
    }
}
using Chatter.Domain.Entities;
using SharedKernel.Interfaces;
using SharedKernel.Results;

namespace Chatter.Domain.Interfaces;

public interface IUserRepository : IRepository<User>
{
    // Task<Result<User>> GetById(Guid userId);
}
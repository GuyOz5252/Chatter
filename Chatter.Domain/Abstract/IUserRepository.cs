using Chatter.Domain.Entities;
using SharedKernel.Results;

namespace Chatter.Domain.Abstract;

public interface IUserRepository
{
    Task<Result<User>> GetAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
    void Create(User user);
    
}

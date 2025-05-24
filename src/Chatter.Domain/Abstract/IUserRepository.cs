using Chatter.Domain.Entities;

namespace Chatter.Domain.Abstract;

public interface IUserRepository
{
    void Create(User user);
}

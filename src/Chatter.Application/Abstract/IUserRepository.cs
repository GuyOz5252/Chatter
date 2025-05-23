using Chatter.Domain.Entities;

namespace Chatter.Application.Abstract;

public interface IUserRepository
{
    void Create(User user);
}

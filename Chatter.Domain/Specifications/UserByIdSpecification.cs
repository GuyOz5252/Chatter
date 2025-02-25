using Chatter.Domain.Entities;
using SharedKernel.Specifications;

namespace Chatter.Domain.Specifications;

public class UserByIdSpecification : SpecificationBase<User>
{
    public UserByIdSpecification(Guid userId)
    {
        Query = user =>
            user.UserId.Equals(userId);
    }
}

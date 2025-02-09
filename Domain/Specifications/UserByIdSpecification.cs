using SharedKernel.Specifications;
using Domain.Models;

namespace Domain.Specifications;

public class UserByIdSpecification : SpecificationBase<User>
{
    public UserByIdSpecification(Guid userId)
    {
        Query = Query
            .Where(user => user.UserId.Equals(userId));
    }
}

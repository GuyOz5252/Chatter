using Chatter.Domain.Entities;
using Chatter.Domain.Interfaces;
using Chatter.Domain.Specifications;
using SharedKernel.Interfaces;
using SharedKernel.Results;

namespace Chatter.Application.Users.Queries.GetUserById;

public class GetUserByIdQueryHandler(IUserRepository userRepository)
    : IQueryHandler<GetUserByIdQuery, User>
{
    private readonly IUserRepository _userRepository = userRepository;
    
    public async Task<Result<User>> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
    {
        return await _userRepository.GetBySpecificationAsync(new UserByIdSpecification(query.UserId), cancellationToken);
    }
}
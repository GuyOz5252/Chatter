using Domain.Interfaces;
using Domain.Specifications;
using SharedKernel.Interfaces;
using SharedKernel.Results;

namespace Application.Users.Queries.GetUserById;

public class GetUserByIdQueryHandler(IUserRepository userRepository)
    : IQueryHandler<GetUserByIdQuery, Domain.Entities.User>
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<Result<Domain.Entities.User>> HandleAsync(GetUserByIdQuery query, CancellationToken cancellationToken = default)
    {
        var userResult = await _userRepository.GetBySpecificationAsync(new UserByIdSpecification(query.UserId), cancellationToken);
        if (userResult.IsFailure)
        {
            return userResult.Error;
        }
        
        return userResult.Value;
    }
}
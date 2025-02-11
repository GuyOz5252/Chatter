using Domain.Interfaces;
using Domain.Specifications;
using SharedKernel.Interfaces;
using SharedKernel.Results;

namespace Application.User.GetById;

public class GetUserByIdQueryHandler(IUserRepository userRepository)
    : IQueryHandler<GetUserByIdQuery, Domain.Models.User>
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<Result<Domain.Models.User>> HandleAsync(GetUserByIdQuery query, CancellationToken cancellationToken = default)
    {
        var userResult = await _userRepository.GetBySpecificationAsync(new UserByIdSpecification(query.UserId), cancellationToken);
        if (userResult.IsFailure)
        {
            return userResult.Error;
        }
        
        return userResult.Value;
    }
}
using Domain.Interfaces;
using SharedKernel.Interfaces;
using SharedKernel.Results;

namespace Application.Users.Commands.CreateUser;

public class CreateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<CreateUserCommand, Guid>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<Guid>> HandleAsync(CreateUserCommand command, CancellationToken cancellationToken = default)
    {
        var user = new Domain.Entities.User(Guid.NewGuid(), command.Username, command.Email);
        var userResult = await _userRepository.AddAsync(user, cancellationToken);
        if (userResult.IsFailure)
        {
            return userResult.Error;
        }
        
        await _unitOfWork.CommitAsync(cancellationToken);
        return userResult.Value.UserId;
    }
}

using Chatter.Application.Abstract;
using Chatter.Domain.Entities;
using SharedKernel;

namespace Chatter.Application.Users.Register;

public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;

    public RegisterUserCommandHandler(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
    }

    public async Task<Result<Guid>> HandleAsync(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        var passwordHash = _passwordHasher.Hash(command.Password);

        var user = new User
        {
            Id = Guid.NewGuid(),
            UserName = command.UserName,
            Email = command.Email,
            PasswordHash = passwordHash
        };

        _userRepository.Create(user);
        await _unitOfWork.CommitAsync(cancellationToken);

        return user.Id;
    }
}

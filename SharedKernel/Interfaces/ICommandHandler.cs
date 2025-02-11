using SharedKernel.Results;

namespace SharedKernel.Interfaces;

public interface ICommandHandler<in TCommand> where TCommand : ICommand 
{
    Task<Result> HandleAsync(TCommand command, CancellationToken cancellationToken = default);
}

public interface ICommandHandler<in TCommand, TResponse> where TCommand : ICommand<TResponse>
{
    Task<Result<TResponse>> HandleAsync(TCommand command, CancellationToken cancellationToken = default);
}
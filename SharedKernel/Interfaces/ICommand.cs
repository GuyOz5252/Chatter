using MediatR;
using SharedKernel.Results;

namespace SharedKernel.Interfaces;

public interface ICommand : IRequest<Result>;

public interface ICommand<TResponse> : IRequest<Result<TResponse>>;

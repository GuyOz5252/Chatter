using MediatR;
using SharedKernel.Results;

namespace SharedKernel.Interfaces;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;
using MediatR;
using SharedKernel.Results;

namespace SharedKernel.Interfaces;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>> where TQuery : IQuery<TResponse>;
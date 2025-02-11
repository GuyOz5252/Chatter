using SharedKernel.Interfaces;
using Domain.Models;

namespace Application.User.GetById;

public record GetUserByIdQuery(Guid UserId) : IQuery<Domain.Models.User>;
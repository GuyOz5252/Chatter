using Chatter.Domain.Entities;
using SharedKernel.Interfaces;

namespace Chatter.Application.Users.Queries.GetUserById;

public record GetUserByIdQuery(Guid UserId) : IQuery<User>;
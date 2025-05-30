using Chatter.Api.Endpoints.Abstract;
using Chatter.Api.Extensions;
using Chatter.Application.Chats.Get;
using Chatter.Domain.Entities;
using SharedKernel.Messaging;

namespace Chatter.Api.Endpoints.Chats;

public class GetChatsEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/chats/{userId:guid}", async (
                Guid userId,
                IQueryHandler<GetChatsQuery, List<Chat>> queryHandler,
                CancellationToken cancellationToken) =>
            {
                var getChatsQuery = new GetChatsQuery
                {
                    UserId = userId,
                };
                var result = await queryHandler.HandleAsync(getChatsQuery, cancellationToken);
                result.Match(
                    Results.Ok,
                    error => error.ToProblemDetails());
            })
        .WithTags(Tags.Chats);
    }
}

using Chatter.Domain.Entities;
using SharedKernel.Specifications;

namespace Chatter.Domain.Specifications;

public class ChatsByUserSpecification : SpecificationBase<Chat>
{
    public ChatsByUserSpecification(Guid userId)
    {
        Query = Query
            .Where(chat => chat.Participants
                .Any(participant => participant.UserId.Equals(userId)));
    }
}
using Chatter.Domain.Entities;
using SharedKernel.Specifications;

namespace Chatter.Domain.Specifications;

public class UserIsChatParticipantSpecification : SpecificationBase<Chat>
{ 
    public UserIsChatParticipantSpecification(Guid chatId, Guid userId)
    {
        Query = Query
            .Where(chat => chat.ChatId.Equals(chatId))
            .Where(chat => chat.Participants
                .Any(participant => participant.UserId.Equals(userId)));
    }
}
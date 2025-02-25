using Chatter.Domain.Entities;
using SharedKernel.Specifications;

namespace Chatter.Domain.Specifications;

public class UserIsChatParticipantSpecification : SpecificationBase<Chat>
{ 
    public UserIsChatParticipantSpecification(Guid chatId, Guid userId)
    {
        Query = chat =>
            chat.ChatId.Equals(chatId)
            && chat.Participants
                .Any(participant => participant.UserId.Equals(userId));
    }
}
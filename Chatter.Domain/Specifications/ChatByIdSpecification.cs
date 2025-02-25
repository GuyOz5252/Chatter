using Chatter.Domain.Entities;
using SharedKernel.Specifications;

namespace Chatter.Domain.Specifications;

public class ChatByIdSpecification : SpecificationBase<Chat>
{
    public ChatByIdSpecification(Guid chatId)
    {
        Query = chat => chat.ChatId.Equals(chatId);
    }
}
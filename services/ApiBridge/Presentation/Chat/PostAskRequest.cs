using Domain.Model;

namespace Presentation.Chat;

public record PostAskRequest(
    Message Question,
    List<Message> Context
);
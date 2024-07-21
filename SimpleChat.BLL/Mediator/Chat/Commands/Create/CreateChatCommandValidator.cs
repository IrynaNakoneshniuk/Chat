using FluentValidation;
using SimpleChat.BLL.Mediator.Chats.Commands.Create;

namespace SimpleChat.BLL.Mediator.Chats.Commands.Create;

public class CreateChatCommandValidator : AbstractValidator<CreateChatCommand>
{
    public CreateChatCommandValidator()
    {
        RuleFor(ch => ch.NewChat.ChatName).NotNull().NotEmpty();
        RuleFor(ch => ch.NewChat.ParticipantIds).NotNull();
        RuleFor(ch => ch.NewChat.CreatedByUserId).NotNull().NotEmpty();
    }
}

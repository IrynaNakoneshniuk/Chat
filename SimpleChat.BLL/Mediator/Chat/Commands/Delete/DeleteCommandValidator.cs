using FluentValidation;

namespace SimpleChat.BLL.Mediator.Chats.Commands.Delete;

public class DeleteChatCommandValidator : AbstractValidator<DeleteOrLeaveChatCommand>
{
    public DeleteChatCommandValidator()
    {
        RuleFor(x => x.ChatId).GreaterThan(0).WithMessage("ChatId must be greater than 0.");
        RuleFor(x => x.UserId).GreaterThan(0).WithMessage("UserId must be greater than 0.");
    }
}

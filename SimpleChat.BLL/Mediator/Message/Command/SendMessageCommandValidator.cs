using FluentValidation;

namespace SimpleChat.BLL.Mediator.Messages.Command;

public class SendMessageCommandValidator : AbstractValidator<SendMessageCommand>
{
    public SendMessageCommandValidator()
    {
        RuleFor(x => x.Message.ChatId).GreaterThan(0).WithMessage("ChatId must be greater than 0.");
        RuleFor(x => x.Message.UserId).GreaterThan(0).WithMessage("UserId must be greater than 0.");
        RuleFor(x => x.Message.Content).NotEmpty().WithMessage("Content must not be empty.");
        RuleFor(x => x.Message.Id).Equal(0).WithMessage("Id must be zero when creating a new message.");
    }
}
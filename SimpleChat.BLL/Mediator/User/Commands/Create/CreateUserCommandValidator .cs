using FluentValidation;

namespace SimpleChat.BLL.Mediator.Users.Commands.Create;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.NewUser.UserName)
            .NotEmpty().WithMessage("User name is required.");

        RuleFor(x => x.NewUser.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Email format is invalid.");
    }
}

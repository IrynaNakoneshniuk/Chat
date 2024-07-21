using FluentValidation;

namespace SimpleChat.BLL.Mediator.Chats.Queries.GetAllByUserId;

public class GetAllByIdQueryValidator : AbstractValidator<GetAllByIdQuery>
{
    public GetAllByIdQueryValidator()
    {
        RuleFor(x => x.UserId).GreaterThan(0).WithMessage("UserId must be greater than zero.");
    }
}

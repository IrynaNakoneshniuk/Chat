using FluentResults;
using MediatR;
using SimpleChat.BLL.DTO.User;

namespace SimpleChat.BLL.Mediator.Users.Commands.Create;

public record CreateUserCommand(CreateUserDto NewUser) : IRequest<Result<UserDto>>;


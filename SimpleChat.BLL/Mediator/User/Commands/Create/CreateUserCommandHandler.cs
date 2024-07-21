using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using SimpleChat.BLL.DTO.User;
using SimpleChat.DAL.Models.Users;
using SimpleChat.DAL.Repositories.Interfaces;

namespace SimpleChat.BLL.Mediator.Users.Commands.Create;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<UserDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<CreateUserCommandHandler> _logger;
    private readonly IMapper _mapper;

    public CreateUserCommandHandler(IUserRepository userRepository, ILogger<CreateUserCommandHandler> logger, IMapper mapper)
    {
        _userRepository = userRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<Result<UserDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(request.NewUser);

        if (user == null)
        {

            var errorMsg = "Error mapping CreateUserDto to User.";
            _logger.LogError(errorMsg);
            return Result.Fail<UserDto>(errorMsg);
        }

       var creatingUser = await _userRepository.CreateAsync(user);

        if (await _userRepository.SaveChangesAsync() == 0)
        {
            var errorMsg = "Error saving user to the database.";
            _logger.LogError(errorMsg);
            return Result.Fail<UserDto>(errorMsg);
        }

        var userDto = _mapper.Map<UserDto>(creatingUser);

        return Result.Ok(userDto);
    }
}

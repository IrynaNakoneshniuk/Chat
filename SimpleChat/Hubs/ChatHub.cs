using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using SimpleChat.BLL.DTO.User;
using SimpleChat.BLL.Specification.Users;
using SimpleChat.DAL.Models.Users;
using SimpleChat.DAL.Repositories.Interfaces;

namespace SimpleChat.WebApi.Hubs;

public sealed class ChatHub : Hub
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public ChatHub(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public override async Task OnConnectedAsync()
    {
        try
        {
            var httpContext = Context.GetHttpContext();
            var userId = int.Parse(httpContext.Request.Query["userId"]);

            if (userId == 0)
            {
                await Clients.Caller.SendAsync("onError", "User not found");
            }

            var user = await _userRepository.GetFirstOrDefaulAsync(new FindUserByIdSpec(userId));
            if (user == null)
            {
                await Clients.Caller.SendAsync("onError", "User not found");
                return;
            }

            user.ConnectionId = Context.ConnectionId;
            _userRepository.Update(user);
            await _userRepository.SaveChangesAsync();

            var userDto = _mapper.Map<User, UserDto>(user);

            await Clients.Caller.SendAsync("getProfileInfo", userDto);
        }
        catch (Exception ex)
        {
            await Clients.Caller.SendAsync("onError", "OnConnected: " + ex.Message);
        }

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        try
        {
            var connectionId = Context.ConnectionId;
            var user = await _userRepository.GetFirstOrDefaulAsync(new FindUserByConnectionSpec(connectionId));

            if (user != null)
            {
                user.ConnectionId = null;

                _userRepository.Update(user);
                await _userRepository.SaveChangesAsync();

                await Clients.Others.SendAsync("UserDisconnected", user.Id);
            }
        }
        catch (Exception ex)
        {
            await Clients.Caller.SendAsync("onError", "OnDisconnected: " + ex.Message);
        }

        await base.OnDisconnectedAsync(exception);
    }

}
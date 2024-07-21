using MediatR;
using Microsoft.AspNetCore.Mvc;
using SimpleChat.BLL.DTO.User;
using SimpleChat.BLL.Mediator.Users.Commands.Create;

namespace SimpleChat.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;
    public UserController(IMediator mediator)
    { 
      _mediator = mediator; 
    }


    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserDto newUser)
    {
        var result = await _mediator.Send(new CreateUserCommand(newUser));

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Errors);
    }
}


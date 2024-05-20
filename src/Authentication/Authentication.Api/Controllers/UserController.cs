using Authentication.Infrastructure.Features.Commands.CreateRole;
using Authentication.Infrastructure.Features.Commands.Login;
using Authentication.Infrastructure.Features.Commands.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("create-roles")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateRoles([FromBody] CreateRoleCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
using MediatR;
using Medicar.Configuration;
using Medicar.Domain.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Medicar.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private IIdentityManager _identityManager;

        public AuthController(IMediator mediator, IIdentityManager identityManager)
        {
            _mediator = mediator;
            _identityManager = identityManager;
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> RegisterUser([FromBody] RegistrarUsuarioCommand command)
        {
            var registered = await _mediator.Send(command);

            if (registered)
            {
                return Ok(registered);
            }

            return BadRequest(registered);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUsuarioCommand command)
        {
            var authenticated = await _mediator.Send(command);

            if (authenticated)
            {
                return Ok(await _identityManager.GenerateJwt(command.Email));
            }

            return Ok(authenticated);
        }

        [Authorize]
        [HttpGet("usuarios")]
        public async Task<IActionResult> GetAll()
        {
            var usuarios = await _mediator.Send(new BuscarUsuarioCommand());

            return Ok(usuarios);
        }
    }
}

using MediatR;
using Medicar.Domain.Commands;
using Medicar.Domain.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Medicar.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/agendas")]
    public class AgendasController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AgendasController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost()]
        public async Task<IActionResult> CriarAgenda([FromBody] CriarAgendaCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpGet()]
        public async Task<IActionResult> ListarAgendaDisponivel([FromQuery] ListarAgendaDisponivelQuery query)
        {
            var result = await _mediator.Send(query);

            return Ok(result);
        }
    }
}

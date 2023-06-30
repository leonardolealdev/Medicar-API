using MediatR;
using Medicar.Domain.Commands;
using Medicar.Domain.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Medicar.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/consultas")]
    public class ConsultasController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ConsultasController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpDelete("{horarioId}")]
        public async Task<IActionResult> DesmarcarConsulta([FromRoute] Guid horarioId)
        {
            var result = await _mediator.Send(new DesmarcarConsultaCommand(horarioId));

            return Ok(result);
        }

        [HttpPost()]
        public async Task<IActionResult> AgendarConsulta([FromBody] AgendarConsultaCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpGet()]
        public async Task<IActionResult> ListarConsultas([FromQuery] ListarConsultasQuery query)
        {
            var result = await _mediator.Send(query);

            return Ok(result);
        }
    }
}

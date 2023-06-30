using MediatR;
using Medicar.Domain.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Medicar.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/medicos")]
    public class MedicosController : ControllerBase
    {
        private readonly IMediator _mediator;
        public MedicosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] CriarMedicoCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }        
    }
}

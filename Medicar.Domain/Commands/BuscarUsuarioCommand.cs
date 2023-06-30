using MediatR;
using Medicar.Domain.Responses;

namespace Medicar.Domain.Commands
{
    public class BuscarUsuarioCommand : IRequest<IEnumerable<UsuarioResponse>>
    {
    }
}

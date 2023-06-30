using MediatR;
using Medicar.Domain.Responses;

namespace Medicar.Domain.Query
{
    public class ListarConsultasQuery : IRequest<IEnumerable<ConsultaResponse>>
    {
    }
}

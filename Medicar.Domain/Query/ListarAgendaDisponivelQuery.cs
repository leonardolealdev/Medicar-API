using MediatR;
using Medicar.Domain.Responses;

namespace Medicar.Domain.Query
{
    public class ListarAgendaDisponivelQuery : IRequest<IEnumerable<AgendaDisponivelResponse>>
    {
        public List<Guid>? Medico { get; set; }
        public List<int>? CRM { get; set; }
        public DateTime? Data_Inicio { get; set; }
        public DateTime? Data_Final { get; set; }
    }
}

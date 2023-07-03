using Medicar.Domain.Entities;
using Medicar.Domain.Query;

namespace Medicar.Domain.Interfaces.Repositories
{
    public interface IAgendaRepository : IRepository<Agenda>
    {
        Task<bool> ExistsAgenda(DateTime dia, Guid id);
        Task<Horario> GetHorario(Guid id);
        Task AtualizarHorario(Horario horario);
        Task<IEnumerable<Horario>> ListarConsultas();
        Task<IEnumerable<Horario>> ListarAgendaDisponivel(ListarAgendaDisponivelQuery request);
    }
}

using Medicar.Domain.Entities;
using Medicar.Domain.Interfaces.Repository;
using Medicar.Domain.Query;
using Medicar.Domain.Responses;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Security.Cryptography;

namespace Medicar.Infra.Data.Repositories
{
    public class AgendaRepository : Repository<Agenda>, IAgendaRepository
    {
        public AgendaRepository(MedicarDbContext dbContext) : base(dbContext) { }

        public async Task<bool> ExistsAgenda(DateTime dia, Guid medicoId)
        {
            return await _dbContext.Agendas.AnyAsync(x => x.Dia == dia.Date && x.MedicoId == medicoId);
        }

        public async Task<Horario> GetHorario(Guid id) => await _dbContext.Horarios.Include(x => x.Agenda).FirstOrDefaultAsync(x => x.Id == id);

        public async Task AtualizarHorario(Horario horario)
        {
            var entityEntry = _dbContext.Entry(horario);
            entityEntry.State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Horario>> ListarConsultas()
        {
            return await _dbContext.Horarios.Include(x => x.Agenda)
                                            .Include(x => x.Agenda.Medico)
                                            .Where(x => x.Agenda.Dia >= DateTime.Now.Date && 
                                                        x.DataAgendamento.HasValue)
                                            .OrderBy(x => x.Agenda.Dia)
                                            .ThenBy(x => x.Hora)
                                            .ToListAsync();
        }

        public async Task<IEnumerable<Horario>> ListarAgendaDisponivel(ListarAgendaDisponivelQuery request)
        {
            var query = _dbContext.Horarios.Include(x => x.Agenda)
                                            .Include(x => x.Agenda.Medico)
                                            .Where(x => x.Agenda.Dia >= DateTime.Now.Date &&
                                                        x.DataAgendamento == null).AsQueryable();

            if (request.Medico != null && request.Medico.Any())
                query = query.Where(x => request.Medico.Contains(x.Agenda.MedicoId));
            if (request.CRM != null && request.CRM.Any())
                query =  query.Where(x => request.CRM.Contains(x.Agenda.Medico.CRM));
            if (request.Data_Inicio != null && request.Data_Inicio.HasValue)
                query =  query.Where(x => request.Data_Inicio.Value.Date >= x.Agenda.Dia);
            if (request.Data_Final != null && request.Data_Final.HasValue)
                query =  query.Where(x => x.Agenda.Dia <= request.Data_Final.Value.Date);

            return await query.OrderBy(x => x.Agenda.Dia)
                                .ThenBy(x => x.Hora)
                                .ToListAsync();
        }
    }
}

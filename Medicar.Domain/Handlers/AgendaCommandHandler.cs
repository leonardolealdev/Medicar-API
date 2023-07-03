using MediatR;
using Medicar.Domain.Commands;
using Medicar.Domain.Entities;
using Medicar.Domain.Interfaces.Repositories;
using Medicar.Domain.Interfaces.Services;

namespace Medicar.Domain.Handlers
{
    public class AgendaCommandHandler : 
        IRequestHandler<CriarAgendaCommand, GenericoCommand>,
        IRequestHandler<AgendarConsultaCommand, GenericoCommand>,
        IRequestHandler<DesmarcarConsultaCommand, GenericoCommand>
    {
        private readonly IAgendaRepository _repository;
        private readonly ITrelloService _trelloService;

        public AgendaCommandHandler(IAgendaRepository repository, ITrelloService trelloService)
        {
            _repository = repository;
            _trelloService = trelloService;
        }
        

        public async Task<GenericoCommand> Handle(CriarAgendaCommand request, CancellationToken cancellationToken)
        {
            var agendaExists = await _repository.ExistsAgenda(request.Dia, request.MedicoId);
            if (agendaExists)
                return new GenericoCommand(false, "Já existe agenda para esse dia", null);


            var agenda = new Agenda(request.MedicoId, request.Dia);
            agenda.AdicionarHorarios(request);
            var agendaId = await _repository.CreateAsync(agenda);
            return new GenericoCommand(true, "Agenda salva com sucesso", agendaId);
        }

        public async Task<GenericoCommand> Handle(AgendarConsultaCommand request, CancellationToken cancellationToken)
        {
            var horario = await _repository.GetHorario(request.HorarioId);
            if (horario is null)
                return new GenericoCommand(false, "Horário não encontrado", null );

            horario.Agendar();
            await _repository.AtualizarHorario(horario);
            await _trelloService.CriarCard(horario.Hora, horario.Agenda.Medico.Nome);
            return new GenericoCommand(true, "Consulta marcada com sucesso", null);
        }

        public async Task<GenericoCommand> Handle(DesmarcarConsultaCommand request, CancellationToken cancellationToken)
        {
            var horario = await _repository.GetHorario(request.HorarioId);
            if (horario is null)
                return new GenericoCommand(false, "Horário não encontrado", null);

            if (horario.DataAgendamento == null)
                return new GenericoCommand(false, "Horário ainda não foi agendado", null);

            var dataHoraAgendamento = horario.Agenda.Dia.Add(horario.Hora);
            if (dataHoraAgendamento < DateTime.Now)
                return new GenericoCommand(false, "Não é permitido desmarcar uma consulta que já aconteceu", null);

            horario.Desmarcar();
            await _repository.AtualizarHorario(horario);
            return new GenericoCommand(true, "Consulta desmarcada com sucesso", null);
        }
    }
}

using AutoMapper;
using MediatR;
using Medicar.Domain.Commands;
using Medicar.Domain.Interfaces.Repositories;
using Medicar.Domain.Query;
using Medicar.Domain.Responses;

namespace Medicar.Domain.Handlers
{
    public class AgendaQueryHandler :
         IRequestHandler<ListarConsultasQuery, IEnumerable<ConsultaResponse>>,
        IRequestHandler<ListarAgendaDisponivelQuery, IEnumerable<AgendaDisponivelResponse>>
    {
        private readonly IAgendaRepository _repository;
        private readonly IMapper _mapper;

        public AgendaQueryHandler(IAgendaRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ConsultaResponse>> Handle(ListarConsultasQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<IEnumerable<ConsultaResponse>>(await _repository.ListarConsultas());             
        }

        public async Task<IEnumerable<AgendaDisponivelResponse>> Handle(ListarAgendaDisponivelQuery request, CancellationToken cancellationToken)
        {
            var agenda = await _repository.ListarAgendaDisponivel(request);
            return agenda.GroupBy(x => x.AgendaId).Select(x => new AgendaDisponivelResponse()
            {
                AgendaId = x.Key,
                Dia = x.Select(y => y.Agenda.Dia.ToString("yyyy-MM-dd")).FirstOrDefault(),
                Medico = new MedicoResponse()
                {
                    Id = x.Select(y => y.Agenda.Medico.Id).FirstOrDefault(),
                    Nome = x.Select(y => y.Agenda.Medico.Nome).FirstOrDefault(),
                    CRM = x.Select(y => y.Agenda.Medico.CRM).FirstOrDefault(),
                    Email = x.Select(y => y.Agenda.Medico.Email).FirstOrDefault(),
                },
                Horario = x.Select(y => y.Hora.ToString()).ToList()
            });
        }
    }
}

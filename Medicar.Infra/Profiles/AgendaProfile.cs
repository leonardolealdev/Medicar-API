using AutoMapper;
using Medicar.Domain.Entities;
using Medicar.Domain.Responses;

namespace Medicar.Infra.Profiles
{
    public class AgendaProfile : Profile
    {
        public AgendaProfile() 
        {
            CreateMap<Horario, ConsultaResponse>()
                .ForMember(dest => dest.AgendaId, opt => opt.MapFrom(src => src.AgendaId))
                .ForMember(dest => dest.HorarioId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Dia, opt => opt.MapFrom(src => src.Agenda.Dia.ToString("yyyy-MM-dd")))
                .ForMember(dest => dest.Horario, opt => opt.MapFrom(src => src.Hora))
                .ForMember(dest => dest.Data_Agendamento, opt => opt.MapFrom(src => src.DataAgendamento))
                .ForMember(dest => dest.Medico, opt => opt.MapFrom(src => new MedicoResponse()
                {
                    Id = src.Agenda.MedicoId,
                    Nome = src.Agenda.Medico.Nome,
                    CRM = src.Agenda.Medico.CRM,
                    Email = src.Agenda.Medico.Email != null ? src.Agenda.Medico.Email : ""
                }));

        }
    }
}

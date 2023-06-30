using FluentValidation;
using MediatR;

namespace Medicar.Domain.Commands
{
    public class AgendarConsultaCommand : IRequest<GenericoCommand>
    {
        public AgendarConsultaCommand(Guid medicoId, Guid horarioId) 
        { 
            MedicoId = medicoId;
            HorarioId = horarioId;
        }

        public Guid MedicoId { get; set; }
        public Guid HorarioId { get; set; }
    }

    public class AgendarConsultaCommandValidator : AbstractValidator<AgendarConsultaCommand>
    {
        public AgendarConsultaCommandValidator()
        {
            RuleFor(r => r.MedicoId).NotNull().NotEmpty().WithMessage("O Id do médico é obrigatório");
            RuleFor(r => r.HorarioId).NotNull().NotEmpty().WithMessage("O Id da consulta é obrigatório");                
        }
    }
}

using FluentValidation;
using MediatR;

namespace Medicar.Domain.Commands
{
    public class DesmarcarConsultaCommand : IRequest<GenericoCommand>
    {
        public DesmarcarConsultaCommand(Guid horarioId)
        {
            HorarioId = horarioId;
        }

        public Guid HorarioId { get; set; }
    }

    public class DesmarcarConsultaCommandValidator : AbstractValidator<DesmarcarConsultaCommand>
    {
        public DesmarcarConsultaCommandValidator()
        {
            RuleFor(r => r.HorarioId).NotNull().NotEmpty().WithMessage("O Id da consulta é obrigatório");                
        }
    }
}

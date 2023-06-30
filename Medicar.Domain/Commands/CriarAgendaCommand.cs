using FluentValidation;
using MediatR;

namespace Medicar.Domain.Commands
{
    public class CriarAgendaCommand : IRequest<GenericoCommand>
    {
        public CriarAgendaCommand(Guid medicoId, DateTime dia, List<string> horarios) 
        { 
            MedicoId = medicoId;
            Dia = dia;
            Horarios = horarios;
        }

        public Guid MedicoId { get; set; }
        public DateTime Dia { get; set; }
        public List<string> Horarios { get; set; }
    }

    public class CriarAgendaCommandValidator : AbstractValidator<CriarAgendaCommand>
    {
        public CriarAgendaCommandValidator()
        {
            RuleFor(r => r.MedicoId).NotNull().WithMessage("O Id do médico é obrigatório");
            RuleFor(r => r.Dia).NotNull().WithMessage("O dia é obrigatório")
                .Must(ValidarDia).WithMessage("Dia não pode ser anteior a data atual");
            RuleFor(r => r.Horarios).Must(ValidarHorario).WithMessage("Os horários são obrigatórios");
        }

        private static bool ValidarDia(DateTime dia)
        {
            return dia.Date >= DateTime.Now.Date;
        }

        private static bool ValidarHorario(List<string> horarios)
        {
            return horarios.Count() > 0;
        }
    }
}

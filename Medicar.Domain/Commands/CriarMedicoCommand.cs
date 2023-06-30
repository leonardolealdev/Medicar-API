using FluentValidation;
using MediatR;

namespace Medicar.Domain.Commands
{
    public class CriarMedicoCommand : IRequest<GenericoCommand>
    {
        public CriarMedicoCommand(string nome, int cRM) 
        {
            Nome = nome;
            CRM = cRM;
        }

        public string Nome { get; set; }
        public int CRM { get; set; }
        public string? Email { get; set; }
    }

    public class CriarMedicoCommandValidator : AbstractValidator<CriarMedicoCommand>
    {
        public CriarMedicoCommandValidator()
        {
            RuleFor(r => r.Nome).NotNull().NotEmpty().WithMessage("O Nome é obrigatório");
            RuleFor(r => r.CRM).NotNull().Must(CRMValido).WithMessage("O CRM é obrigatório");
        }

        private static bool CRMValido(int cRM)
        {
            return cRM > 0;
        }
    }
}

using MediatR;
using Medicar.Domain.Commands;
using Medicar.Domain.Entities;
using Medicar.Domain.Interfaces.Repository;

namespace Medicar.Domain.Handlers
{
    public class MedicoCommandHandler : 
        IRequestHandler<CriarMedicoCommand, GenericoCommand>
    {
        private readonly IMedicoRepository _repository;

        public MedicoCommandHandler(IMedicoRepository repository)
        {
            _repository = repository;
        }

        public async Task<GenericoCommand> Handle(CriarMedicoCommand request, CancellationToken cancellationToken)
        {
            var medicoExists = await _repository.ExistsMedico(request.CRM, Guid.Empty);
            if (medicoExists)
                return new GenericoCommand(false, "CRM já existente", null);

            var medico = new Medico(request.Nome, request.CRM, request.Email);
            var medicoId = await _repository.CreateAsync(medico);
            return new GenericoCommand(true, "Medico salvo com sucesso", medicoId);
        }
    }
}

using Medicar.Domain.Commands;

namespace Medicar.Tests.Model
{
    public class MedicoModelBuilder
    {
        public CriarMedicoCommand Build()
        {
            return new CriarMedicoCommand("Medico Teste", 123456);
        }
    }
}

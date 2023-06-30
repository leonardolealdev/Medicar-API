using Medicar.Domain.Commands;

namespace Medicar.Tests.Model
{
    public class DesmarcarModelBuilder
    {
        public DesmarcarConsultaCommand Build()
        {
            return new DesmarcarConsultaCommand(Guid.NewGuid());
        }
    }
}

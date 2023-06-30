using Medicar.Domain.Commands;

namespace Medicar.Tests.Model
{
    public class AgendaModelBuilder
    {
        public CriarAgendaCommand Build()
        {
            var horarios = new List<string>();
            horarios.Add("10:00");
            return new CriarAgendaCommand(Guid.NewGuid(), DateTime.Now, horarios);
        }

        public AgendarConsultaCommand BuildAgendar()
        {
            return new AgendarConsultaCommand(Guid.NewGuid(), Guid.NewGuid());
        }
    }
}

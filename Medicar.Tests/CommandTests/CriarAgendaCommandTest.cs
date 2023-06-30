using Medicar.Domain.Commands;
using Medicar.Tests.Model;

namespace Medicar.Tests.CommandTests
{
    public class CriarAgendaCommandTest
    {
        private readonly CriarAgendaCommand _agenda = new AgendaModelBuilder().Build();
        private readonly CriarAgendaCommandValidator _validador = new CriarAgendaCommandValidator();

        [Fact]
        public void CommandDeveSerValido()
        {
            var result = _validador.Validate(_agenda);

            Assert.True(result.IsValid);
        }

        [Fact]
        public void CommandDeveSerInvalido_HorarioVazio()
        {
            _agenda.Horarios = new List<string>();
            var result = _validador.Validate(_agenda);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void CommandDeveSerInvalido_DiaInvalido()
        {
            _agenda.Dia = DateTime.Now.AddDays(-1);
            var result = _validador.Validate(_agenda);

            Assert.False(result.IsValid);
        }
    }
}

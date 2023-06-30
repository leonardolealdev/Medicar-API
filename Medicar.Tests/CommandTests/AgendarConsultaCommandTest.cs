using Medicar.Domain.Commands;
using Medicar.Tests.Model;

namespace Medicar.Tests.CommandTests
{
    public class AgendarConsultaCommandTest
    {
        private readonly AgendarConsultaCommand _agendar = new AgendaModelBuilder().BuildAgendar();
        private readonly AgendarConsultaCommandValidator _validador = new AgendarConsultaCommandValidator();

        [Fact]
        public void CommandDeveSerValido()
        {
            var result = _validador.Validate(_agendar);

            Assert.True(result.IsValid);
        }

        [Fact]
        public void CommandDeveSerInvalido_HorarioVazio()
        {
            _agendar.HorarioId = Guid.Empty;
            var result = _validador.Validate(_agendar);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void CommandDeveSerInvalido_DiaInvalido()
        {
            _agendar.MedicoId = Guid.Empty;
            var result = _validador.Validate(_agendar);

            Assert.False(result.IsValid);
        }
    }
}

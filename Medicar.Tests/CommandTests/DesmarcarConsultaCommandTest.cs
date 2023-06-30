using Medicar.Domain.Commands;
using Medicar.Tests.Model;

namespace Medicar.Tests.CommandTests
{
    public class DesmarcarConsultaCommandTest
    {
        private readonly DesmarcarConsultaCommand _agendar = new DesmarcarModelBuilder().Build();
        private readonly DesmarcarConsultaCommandValidator _validador = new DesmarcarConsultaCommandValidator();

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
    }
}

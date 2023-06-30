using Medicar.Domain.Commands;
using Medicar.Tests.Model;

namespace Medicar.Tests.CommandTests
{
    public class CriarMedicoCommandTest
    {
        private readonly CriarMedicoCommand _medico = new MedicoModelBuilder().Build();
        private readonly CriarMedicoCommandValidator _validador = new CriarMedicoCommandValidator();

        [Fact]
        public void CommandDeveSerValido()
        {
            var result = _validador.Validate(_medico);

            Assert.True(result.IsValid);
        }

        [Fact]
        public void CommandDeveSerInvalido_NomeVazio()
        {
            _medico.Nome = null;
            var result = _validador.Validate(_medico);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void CommandDeveSerInvalido_CRMVazio()
        {
            _medico.CRM = 0;
            var result = _validador.Validate(_medico);

            Assert.False(result.IsValid);
        }
    }
}

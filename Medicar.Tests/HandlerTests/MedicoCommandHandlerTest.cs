using Medicar.Domain.Commands;
using Medicar.Domain.Handlers;
using Medicar.Domain.Interfaces.Repositories;
using Moq;
using System.Reflection.Metadata;

namespace Medicar.Tests.HandlerTests
{
    public class MedicoCommandHandlerTest
    {
        [Fact]
        public async Task CriarMedico_Sucesso()
        {
            var command = new CriarMedicoCommand("Teste médico", 12345);
            Mock<IMedicoRepository> mockMedicoRepository = new Mock<IMedicoRepository>();
            var handler = new MedicoCommandHandler(mockMedicoRepository.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.True(result.Success);
        }

        [Fact]
        public async Task CriarMedico_Falha_MedicoExistente()
        {
            var command = new CriarMedicoCommand("Teste médico", 12345);
            Mock<IMedicoRepository> mockMedicoRepository = new Mock<IMedicoRepository>();
            mockMedicoRepository.Setup(x => x.ExistsMedico(It.IsAny<int>(), It.IsAny<Guid>())).Returns(Task.FromResult(true));
            var handler = new MedicoCommandHandler(mockMedicoRepository.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.False(result.Success);
        }
    }
}

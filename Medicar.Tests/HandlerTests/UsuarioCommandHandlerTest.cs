using Medicar.Domain.Commands;
using Medicar.Domain.Handlers;
using Medicar.Domain.Interfaces.Repository;
using Moq;

namespace Medicar.Tests.HandlerTests
{
    public class UsuarioCommandHandlerTest
    {
        [Fact]
        public async void ListarUsuarios_Sucesso()
        {
            var command = new BuscarUsuarioCommand();
            Mock<IUsuarioRepository> mockUsuarioRepository = new Mock<IUsuarioRepository>();
            var handler = new UsuarioCommandHandler(mockUsuarioRepository.Object, null, null);
            var result = await handler.Handle(command, CancellationToken.None);

            result.Any();
        }
    }
}

using Medicar.Domain.Commands;
using Medicar.Domain.Entities;
using Medicar.Domain.Handlers;
using Medicar.Domain.Interfaces.Repositories;
using Medicar.Domain.Interfaces.Services;
using Moq;
using System.Security.Cryptography;

namespace Medicar.Tests.HandlerTests
{
    public class AgendaCommandHandlerTest
    {
        [Fact]
        public async Task CriarAgenda_Sucesso()
        {
            var horarios = new List<string>();
            horarios.Add("10:00");
            horarios.Add("11:00");
            var agenda = new Agenda(Guid.NewGuid(), DateTime.Now);
            var command = new CriarAgendaCommand(Guid.NewGuid(), DateTime.Now, horarios);
            agenda.AdicionarHorarios(command);

            Mock<IAgendaRepository> mockAgendaRepository = new Mock<IAgendaRepository>();
            mockAgendaRepository.Setup(x => x.ExistsAgenda(It.IsAny<DateTime>(), It.IsAny<Guid>())).Returns(Task.FromResult(false));
            var handler = new AgendaCommandHandler(mockAgendaRepository.Object, null);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.True(result.Success);
            mockAgendaRepository.Verify(repo => repo.ExistsAgenda(It.IsAny<DateTime>(), It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task CriarAgenda_Falha_AgendaExistente()
        {
            var horarios = new List<string>();
            horarios.Add("10:00");
            horarios.Add("11:00");
            var agenda = new Agenda(Guid.NewGuid(), DateTime.Now);
            var command = new CriarAgendaCommand(Guid.NewGuid(), DateTime.Now, horarios);
            agenda.AdicionarHorarios(command);

            Mock<IAgendaRepository> mockAgendaRepository = new Mock<IAgendaRepository>();
            mockAgendaRepository.Setup(x => x.ExistsAgenda(It.IsAny<DateTime>(), It.IsAny<Guid>())).Returns(Task.FromResult(true));
            var handler = new AgendaCommandHandler(mockAgendaRepository.Object, null);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.False(result.Success);
            mockAgendaRepository.Verify(repo => repo.ExistsAgenda(It.IsAny<DateTime>(), It.IsAny<Guid>()), Times.Once);
            mockAgendaRepository.Verify(repo => repo.CreateAsync(agenda), Times.Never);
        }

        [Fact]
        public async Task AgendarConsulta_Sucesso()
        {
            var hora = new TimeSpan(7, 36, 10);
            var horario = new Horario(hora, DateTime.Now, Guid.NewGuid(), "Medico Teste", 1245, "teste@teste.com.br");
            var command = new AgendarConsultaCommand(Guid.NewGuid(), Guid.NewGuid());
            horario.Agendar();

            Mock<IAgendaRepository> mockAgendaRepository = new Mock<IAgendaRepository>();
            Mock<ITrelloService> mockTrelloService = new Mock<ITrelloService>();
            mockAgendaRepository.Setup(x => x.GetHorario(It.IsAny<Guid>())).Returns(Task.FromResult(horario));
            mockTrelloService.Setup(x => x.CriarCard(It.IsAny<TimeSpan>(), It.IsAny<string>())).Returns(Task.FromResult(true));
            var handler = new AgendaCommandHandler(mockAgendaRepository.Object, mockTrelloService.Object);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.True(result.Success);
            mockAgendaRepository.Verify(repo => repo.GetHorario(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task AgendarConsulta_Falha_HorarioNaoEncontrado()
        {
            var command = new AgendarConsultaCommand(Guid.NewGuid(), Guid.NewGuid());

            Mock<IAgendaRepository> mockAgendaRepository = new Mock<IAgendaRepository>();
            Mock<ITrelloService> mockTrelloService = new Mock<ITrelloService>();
            mockAgendaRepository.Setup(x => x.GetHorario(It.IsAny<Guid>())).Returns(Task.FromResult<Horario>(null));
            mockTrelloService.Setup(x => x.CriarCard(It.IsAny<TimeSpan>(), It.IsAny<string>())).Returns(Task.FromResult(true));
            var handler = new AgendaCommandHandler(mockAgendaRepository.Object, mockTrelloService.Object);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.False(result.Success);
            mockAgendaRepository.Verify(repo => repo.GetHorario(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task DesmarcarConsulta_Sucesso()
        {
            var hora = new TimeSpan(7, 36, 10);
            var horario = new Horario(hora, DateTime.Now, Guid.NewGuid());
            horario.Agendar();
            var command = new DesmarcarConsultaCommand(Guid.NewGuid());

            Mock<IAgendaRepository> mockAgendaRepository = new Mock<IAgendaRepository>();
            mockAgendaRepository.Setup(x => x.GetHorario(It.IsAny<Guid>())).Returns(Task.FromResult(horario));
            var handler = new AgendaCommandHandler(mockAgendaRepository.Object, null);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.True(result.Success);
            mockAgendaRepository.Verify(repo => repo.GetHorario(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task DesmarcarConsulta_Falha_HorarioNaoEncontrado()
        {
            var hora = new TimeSpan(7, 36, 10);
            var horario = new Horario(hora);
            var command = new AgendarConsultaCommand(Guid.NewGuid(), Guid.NewGuid());

            Mock<IAgendaRepository> mockAgendaRepository = new Mock<IAgendaRepository>();
            mockAgendaRepository.Setup(x => x.GetHorario(It.IsAny<Guid>())).Returns(Task.FromResult<Horario>(null));
            var handler = new AgendaCommandHandler(mockAgendaRepository.Object, null);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.False(result.Success);
            mockAgendaRepository.Verify(repo => repo.GetHorario(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task DesmarcarConsulta_Falha_HorarioNaoAgendado()
        {
            var hora = new TimeSpan(7, 36, 10);
            var horario = new Horario(hora, DateTime.Now, Guid.NewGuid());
            horario.Desmarcar();
            var command = new DesmarcarConsultaCommand(Guid.NewGuid());

            Mock<IAgendaRepository> mockAgendaRepository = new Mock<IAgendaRepository>();
            mockAgendaRepository.Setup(x => x.GetHorario(It.IsAny<Guid>())).Returns(Task.FromResult(horario));
            var handler = new AgendaCommandHandler(mockAgendaRepository.Object, null);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.False(result.Success);
            mockAgendaRepository.Verify(repo => repo.GetHorario(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task DesmarcarConsulta_Falha_ConsultaJaAconteceu()
        {
            var hora = new TimeSpan(7, 36, 10);
            var horario = new Horario(hora, DateTime.Now.AddDays(-1), Guid.NewGuid());
            horario.Agendar();
            var command = new DesmarcarConsultaCommand(Guid.NewGuid());

            Mock<IAgendaRepository> mockAgendaRepository = new Mock<IAgendaRepository>();
            mockAgendaRepository.Setup(x => x.GetHorario(It.IsAny<Guid>())).Returns(Task.FromResult(horario));
            var handler = new AgendaCommandHandler(mockAgendaRepository.Object, null);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.False(result.Success);
            mockAgendaRepository.Verify(repo => repo.GetHorario(It.IsAny<Guid>()), Times.Once);
        }
    }
}

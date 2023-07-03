namespace Medicar.Domain.Interfaces.Services
{
    public interface ITrelloService
    {
        Task<bool> CriarCard(TimeSpan horario, string nomeMedico);
    }
}

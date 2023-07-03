using Medicar.Domain.Interfaces.Services;

namespace Medicar.Infra.Services
{
    public class TrelloService : ITrelloService
    {
        private readonly TrelloSettings _trelloSeggins;

        public TrelloService(TrelloSettings trelloSettins)
        {
            _trelloSeggins = trelloSettins;
        }

        public async Task<bool> CriarCard(TimeSpan horario, string nomeMedico)
        {
            using (var httpClient = new HttpClient())
            {
                var nome = $"{nomeMedico} - {horario.ToString()}";
                var descricao = $"Consulta marcara para {nomeMedico} às {horario.ToString()}";
                string url = $"{_trelloSeggins.BaseUrl}?key={_trelloSeggins.AppKey}&token={_trelloSeggins.UserToken}&idList={_trelloSeggins.ListId}&name={nome}&desc={descricao}";

                var response = await httpClient.PostAsync(url, null);

                if (response.IsSuccessStatusCode)
                    return true;
                else
                    return false;
            }
        }
    }
}

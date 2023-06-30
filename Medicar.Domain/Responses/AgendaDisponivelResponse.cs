namespace Medicar.Domain.Responses
{
    public class AgendaDisponivelResponse
    {
        public Guid AgendaId { get; set; }
        public string Dia { get; set; }
        public List<string> Horario { get; set; }
        public MedicoResponse Medico { get; set; }
    }
}

namespace Medicar.Domain.Responses
{
    public class ConsultaResponse
    {
        public Guid AgendaId { get; set; }
        public Guid HorarioId { get; set; }
        public string Dia { get; set; }
        public string Horario { get; set; }
        public DateTime Data_Agendamento { get; set; }
        public MedicoResponse Medico { get; set; }
    }

    public class MedicoResponse
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public int CRM { get; set; }
        public string Email { get; set; }
    }
}

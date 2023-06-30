namespace Medicar.Domain.Entities
{
    public class Horario : EntityBase
    {
        public Horario(TimeSpan hora) 
        {
            Hora = hora;
        }

        public Guid AgendaId { get; private set; }
        public TimeSpan Hora { get; private set; }
        public DateTime? DataAgendamento { get; private set; }
        public Agenda Agenda { get; private set;}

        public void Agendar()
        {
            DataAgendamento = DateTime.Now;
        }

        public void Desmarcar()
        {
            DataAgendamento = null;
        }
    }
}

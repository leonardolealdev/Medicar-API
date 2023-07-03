namespace Medicar.Domain.Entities
{
    public class Horario : EntityBase
    {
        public Horario(TimeSpan hora) 
        {
            Hora = hora;
        }

        public Horario(TimeSpan hora, DateTime dia, Guid medicoId)
        {
            Hora = hora;
            Agenda = new Agenda(medicoId, dia);
        }

        public Horario(TimeSpan hora, DateTime dia, Guid medicoId, string nomeMedico, int crmMedico, string emailMedico)
        {
            Hora = hora;
            Agenda = new Agenda(medicoId, dia);
            Agenda.Medico = new Medico(nomeMedico, crmMedico, emailMedico);
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

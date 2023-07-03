using Medicar.Domain.Commands;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medicar.Domain.Entities
{
    public class Agenda : EntityBase
    {
        private Agenda() { }
        public Agenda(Guid medicoId, DateTime dia)
        {
            MedicoId = medicoId;
            Dia = dia;
        }

        public Guid MedicoId { get; private set; }
        [Column(TypeName = "Date")]
        public DateTime Dia { get; private set; }
        public Medico Medico { get; set; }
        public List<Horario> Horarios { get; private set; } = new List<Horario>();

        public void AdicionarHorarios(CriarAgendaCommand command)
        {
            foreach (var horario in command.Horarios)
                Horarios.Add(new Horario(TimeSpan.Parse(horario)));
        }
    }
}

namespace Medicar.Domain.Entities
{
    public class Medico : EntityBase
    {
        public Medico(string nome, int cRM, string email) 
        {
            Nome = nome;
            CRM = cRM;
            Email = email;
        }

        public string Nome { get; private set; }
        public int CRM { get; private set; }
        public string? Email { get; private set; }
        public List<Agenda> Agendas { get; private set; } = new List<Agenda>();
    }
}

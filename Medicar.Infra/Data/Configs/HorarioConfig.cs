using Medicar.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Medicar.Infra.Data.Configs
{
    public class HorarioConfig : IEntityTypeConfiguration<Horario>
    {
        public void Configure(EntityTypeBuilder<Horario> builder)
        {
            builder.HasKey(x => new { x.Id });
            builder.HasOne(c => c.Agenda).WithMany(a => a.Horarios).HasForeignKey(x => x.AgendaId).IsRequired();
            builder.Property(x => x.Hora).IsRequired();
            builder.Property(x => x.DataAgendamento);
            builder.ToTable("Horarios");
        }
    }
}

using Medicar.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Medicar.Infra.Data.Configs
{
    public class AgendaConfig : IEntityTypeConfiguration<Agenda>
    {
        public void Configure(EntityTypeBuilder<Agenda> builder)
        {
            builder.HasKey(x => new { x.Id });
            builder.HasOne(c => c.Medico).WithMany(a => a.Agendas).HasForeignKey(x => x.MedicoId).IsRequired();
            builder.Property(x => x.Dia).HasColumnType("Date").IsRequired();
            builder.ToTable("Agendas");
        }
    }
}

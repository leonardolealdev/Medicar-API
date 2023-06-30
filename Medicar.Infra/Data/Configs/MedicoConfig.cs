using Medicar.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Medicar.Infra.Data.Configs
{
    public class MedicoConfig : IEntityTypeConfiguration<Medico>
    {
        public void Configure(EntityTypeBuilder<Medico> builder)
        {
            builder.HasKey(x => new { x.Id });
            builder.Property(x => x.Nome).IsRequired();
            builder.Property(x => x.CRM).IsRequired();
            builder.Property(x => x.Email);
            builder.ToTable("Medicos");
        }
    }
}

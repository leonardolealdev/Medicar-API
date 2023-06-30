using Medicar.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Medicar.Infra.Data
{
    public class MedicarDbContext : DbContext
    {
        #region DBSets
        public DbSet<Medico> Medicos { get; set; }
        public DbSet<Agenda> Agendas { get; set; }
        public DbSet<Horario> Horarios { get; set; }
        #endregion

        public MedicarDbContext(DbContextOptions<MedicarDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}

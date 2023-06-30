using Medicar.Domain.Entities;
using Medicar.Domain.Interfaces.Repository;

namespace Medicar.Infra.Data.Repositories
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(MedicarDbContext ctx) : base(ctx) { }

    }
}

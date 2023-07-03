using Medicar.Domain.Entities;
using Medicar.Domain.Interfaces.Repositories;

namespace Medicar.Infra.Data.Repositories
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(MedicarDbContext ctx) : base(ctx) { }

    }
}

using Medicar.Domain.Entities;
using Medicar.Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace Medicar.Infra.Data.Repositories
{
    public class MedicoRepository : Repository<Medico>, IMedicoRepository
    {
        public MedicoRepository(MedicarDbContext ctx) : base(ctx) { }

        public async Task<bool> ExistsMedico(int cRM, Guid id)
        {
            if (id == Guid.Empty)
                return await _dbContext.Medicos.AnyAsync(x => x.CRM == cRM);
            else
                return await _dbContext.Medicos.AnyAsync(x => x.CRM == cRM && x.Id != id);
        }

    }
}

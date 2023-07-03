using Medicar.Domain.Entities;

namespace Medicar.Domain.Interfaces.Repositories
{
    public interface IMedicoRepository : IRepository<Medico>
    {
        Task<bool> ExistsMedico(int cRM, Guid id);
    }
}

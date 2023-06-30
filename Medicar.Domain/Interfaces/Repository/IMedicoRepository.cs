using Medicar.Domain.Entities;

namespace Medicar.Domain.Interfaces.Repository
{
    public interface IMedicoRepository : IRepository<Medico>
    {
        Task<bool> ExistsMedico(int cRM, Guid id);
    }
}

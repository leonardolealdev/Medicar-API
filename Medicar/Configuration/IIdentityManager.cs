using Medicar.Domain.Responses;

namespace Medicar.Configuration
{
    public interface IIdentityManager
    {
        Task<LoginResponse> GenerateJwt(string email);
    }
}

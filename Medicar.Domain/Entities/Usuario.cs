using Microsoft.AspNetCore.Identity;

namespace Medicar.Domain.Entities
{
    public class Usuario : IdentityUser<Guid>
    {
        public string Nome { get; set; }
    }
}

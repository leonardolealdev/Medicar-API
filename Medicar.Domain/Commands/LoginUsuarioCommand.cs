using MediatR;

namespace Medicar.Domain.Commands
{
    public class LoginUsuarioCommand : IRequest<bool>
    {
        public string Email { get; set; }
        public string Password { get; set; }

    }
}

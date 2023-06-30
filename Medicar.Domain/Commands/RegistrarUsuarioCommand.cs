using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Medicar.Domain.Commands
{
    public class RegistrarUsuarioCommand : IRequest<bool>
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "As senhas não conferem")]
        public string ConfirmPassword { get; set; }
    }
}

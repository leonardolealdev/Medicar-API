using MediatR;
using Medicar.Domain.Commands;
using Medicar.Domain.Entities;
using Medicar.Domain.Interfaces.Repositories;
using Medicar.Domain.Responses;
using Microsoft.AspNetCore.Identity;

namespace Medicar.Domain.Handlers
{
    public class UsuarioCommandHandler : 
        IRequestHandler<BuscarUsuarioCommand, IEnumerable<UsuarioResponse>>,
        IRequestHandler<LoginUsuarioCommand, bool>,
        IRequestHandler<RegistrarUsuarioCommand, bool>
    {
        private readonly IUsuarioRepository _repository;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly UserManager<Usuario> _userManager;
        public UsuarioCommandHandler(
            IUsuarioRepository repository, 
            SignInManager<Usuario> signInManager,
            UserManager<Usuario> userManager)
        {
            _repository = repository;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<IEnumerable<UsuarioResponse>> Handle(BuscarUsuarioCommand request, CancellationToken cancellationToken)
        {
            var usuarios = await _repository.GetAllAsync();

            var result = usuarios.AsQueryable().Select(p => new UsuarioResponse
            {
                Id = p.Id,
                Nome = p.Nome
            });

            return result;
        }

        public async Task<bool> Handle(LoginUsuarioCommand request, CancellationToken cancellationToken)
        {
            var result = await _signInManager.PasswordSignInAsync(request.Email, request.Password, false, true);

            if (result.Succeeded)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> Handle(RegistrarUsuarioCommand request, CancellationToken cancellationToken)
        {
            var user = new Usuario
            {
                Nome = request.Nome,
                UserName = request.Email,
                Email = request.Email,
                EmailConfirmed = true,
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);

                return true;
            }

            return false;
        }
    }
}

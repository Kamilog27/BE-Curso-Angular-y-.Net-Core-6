using BackEndCuestionario.Domain.IRepositories;
using BackEndCuestionario.Domain.IServices;
using BackEndCuestionario.Domain.Models;

namespace BackEndCuestionario.Services
{
    public class UsuarioService:IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            this._usuarioRepository = usuarioRepository;
        }

        public async Task SaveUser(Usuario usuario)
        {
            await _usuarioRepository.SaveUser(usuario);
        }


        public async Task<bool> ValidateExistence(Usuario usuario)
        {
            return await _usuarioRepository.ValidateExistence(usuario);
        }

        public async Task<Usuario> ValidatePassword(int idUsario, string passwordAnterior)
        {
            return await _usuarioRepository.ValidatePassword(idUsario, passwordAnterior);
        }

        public async Task UpdatePassword(Usuario usuario)
        {
            await _usuarioRepository.UpdatePassword(usuario);
        }
    }
}

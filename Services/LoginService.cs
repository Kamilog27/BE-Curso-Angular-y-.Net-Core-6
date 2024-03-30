using BackEndCuestionario.Domain.IRepositories;
using BackEndCuestionario.Domain.IServices;
using BackEndCuestionario.Domain.Models;

namespace BackEndCuestionario.Services
{
    public class LoginService:ILoginService
    {
        private readonly ILoginRepository _loginRepository;

        public LoginService(ILoginRepository loginRepository)
        {
            this._loginRepository = loginRepository;
        }

        public async Task<Usuario> ValidateUser(Usuario usuario)
        {
            return await _loginRepository.ValidateUser(usuario);
        }
    }
}

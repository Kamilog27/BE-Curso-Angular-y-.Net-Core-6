using BackEndCuestionario.Domain.Models;

namespace BackEndCuestionario.Domain.IServices
{
    public interface ILoginService
    {
        Task<Usuario> ValidateUser(Usuario usuario);
    }
}

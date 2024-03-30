using BackEndCuestionario.Domain.Models;

namespace BackEndCuestionario.Domain.IRepositories
{
    public interface ILoginRepository
    {
        Task<Usuario> ValidateUser(Usuario usuario);
    }
}

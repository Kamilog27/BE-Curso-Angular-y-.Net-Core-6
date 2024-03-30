using BackEndCuestionario.Domain.Models;

namespace BackEndCuestionario.Domain.IServices
{
    public interface IUsuarioService
    {
        Task SaveUser(Usuario usuario);
        Task<bool> ValidateExistence(Usuario usuario);
        Task<Usuario> ValidatePassword(int idUsario,string passwordAnterior);
        Task UpdatePassword(Usuario usuario);
    }
}

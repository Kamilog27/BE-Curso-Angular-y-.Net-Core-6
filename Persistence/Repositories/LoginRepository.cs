using BackEndCuestionario.Domain.IRepositories;
using BackEndCuestionario.Domain.Models;
using BackEndCuestionario.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace BackEndCuestionario.Persistence.Repositories
{
    public class LoginRepository:ILoginRepository
    {
        private readonly ApplicationDbContext _context;

        public LoginRepository(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task<Usuario> ValidateUser(Usuario usuario)
        {
            var user=await _context.Usuario.Where(x=>x.NombreUsuario==usuario.NombreUsuario
            && x.Contraseña==usuario.Contraseña).FirstOrDefaultAsync();
            return user;
        }
    }
}

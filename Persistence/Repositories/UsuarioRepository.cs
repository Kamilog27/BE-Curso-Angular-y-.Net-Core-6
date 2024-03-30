using BackEndCuestionario.Domain.IRepositories;
using BackEndCuestionario.Domain.Models;
using BackEndCuestionario.Persistence.Context;
using Microsoft.EntityFrameworkCore;


namespace BackEndCuestionario.Persistence.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext _context;

        public UsuarioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SaveUser(Usuario usuario)
        {
            _context.Add(usuario);
            await _context.SaveChangesAsync();
        }

       

        public async Task<bool> ValidateExistence(Usuario usuario)
        {
           var validateExistence=await _context.Usuario.AnyAsync(x=>x.NombreUsuario==usuario.NombreUsuario);
           return validateExistence;
        }

        public async Task<Usuario> ValidatePassword(int idUsuario, string passwordAnterior)
        {
            var usuario=await _context.Usuario.Where(x=>x.Id==idUsuario&&x.Contraseña==passwordAnterior).FirstOrDefaultAsync();
            return usuario;
        }
        public async Task UpdatePassword(Usuario usuario)
        {
            _context.Update(usuario);
            await _context.SaveChangesAsync();
        }
    }
}

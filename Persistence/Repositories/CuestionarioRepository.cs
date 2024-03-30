using BackEndCuestionario.Domain.IRepositories;
using BackEndCuestionario.Domain.Models;
using BackEndCuestionario.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace BackEndCuestionario.Persistence.Repositories
{
    public class CuestionarioRepository:ICuestionarioRepository
    {
        private readonly ApplicationDbContext context;

        public CuestionarioRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task CreateCuestionario(Cuestionario cuestionario)
        {
            this.context.Add(cuestionario);
            await this.context.SaveChangesAsync();
        }

      
        public async Task<List<Cuestionario>> GetListCuestionarioByUser(int idUsuario)
        {
            return await this.context.Cuestionario.Where(x=>x.Activo==1&&x.UsuarioId==idUsuario).ToListAsync();
        }
        public async Task<Cuestionario> GetListCuestionario(int idCuestionario)
        {
            var cuestionario=await this.context.Cuestionario.Where(x=>x.Id==idCuestionario&&x.Activo==1)
                .Include(x=>x.listPreguntas)
                .ThenInclude(x=>x.listRespuesta)
                .FirstOrDefaultAsync();
            return cuestionario;
        }

        public async Task<Cuestionario> BuscarCuestionario(int idCuestionario,int idUsuario)
        {
            var cuestionario = await this.context.Cuestionario.Where(x => x.Id == idCuestionario && x.Activo == 1&&x.UsuarioId==idUsuario).FirstOrDefaultAsync();
            return cuestionario;
        }

        public async Task EliminarCuestionario(Cuestionario cuestionario)
        {
            cuestionario.Activo = 0;
            this.context.Entry(cuestionario).State = EntityState.Modified;
            await this.context.SaveChangesAsync();
        }

        public async Task<List<Cuestionario>> GetListCuestionarios()
        {
            var cuestionarios=await this.context.Cuestionario.Where(x=>x.Activo==1)
                .Select(x=>new Cuestionario { 
                    Id=x.Id,
                    Nombre=x.Nombre,
                    Descripcion=x.Descripcion,
                    FechaCreacion=x.FechaCreacion,
                    Usuario=new Usuario { NombreUsuario=x.Usuario.NombreUsuario} }).ToListAsync();
            return cuestionarios;
        }
    }
}

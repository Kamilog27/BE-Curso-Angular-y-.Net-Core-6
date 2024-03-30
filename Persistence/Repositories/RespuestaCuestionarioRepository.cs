using BackEndCuestionario.Domain.IRepositories;
using BackEndCuestionario.Domain.Models;
using BackEndCuestionario.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace BackEndCuestionario.Persistence.Repositories
{
    public class RespuestaCuestionarioRepository:IRespuestaCuestionarioRepository
    {
        private readonly ApplicationDbContext context;

        public RespuestaCuestionarioRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

      
        public async Task SaveRespuestaCuestionario(RespuestaCuestionario respuestaCuestionario)
        {
            respuestaCuestionario.Activo = 1;
            respuestaCuestionario.Fecha = DateTime.Now;
            this.context.Add(respuestaCuestionario);
            await this.context.SaveChangesAsync();
        }
        public async Task<List<RespuestaCuestionario>> ListRespuestaCuestionario(int idCuestionario, int idUsuario)
        {
            var listRespuestaCuestionario = await this.context.RespuestaCuestionario.Where(x => x.CuestionarioId == idCuestionario && 
            x.Activo == 1 && x.Cuestionario.UsuarioId == idUsuario).OrderByDescending(x => x.Fecha).ToListAsync();
            return listRespuestaCuestionario;

        }

        public async Task<RespuestaCuestionario> BuscarRespuestaCuestionario(int idRtaCuestionario, int idUsuario)
        {
           var respuestaCuestionario=await this.context.RespuestaCuestionario.Where(x=>x.Id==idRtaCuestionario&&x.Cuestionario.UsuarioId==idUsuario&&x.Activo==1).FirstOrDefaultAsync();
            return respuestaCuestionario;
        }

        public async Task EliminarRespuestaCuestionario(RespuestaCuestionario respuestaCuestionario)
        {
            respuestaCuestionario.Activo = 0;
            this.context.Entry(respuestaCuestionario).State=EntityState.Modified;
            await this.context.SaveChangesAsync();
        }

        public async Task<int> GetIdCuestionarioByIdRespuesta(int idRespuestaCuestionario)
        {
            var cuestionario = await this.context.RespuestaCuestionario.Where(x => x.Id == idRespuestaCuestionario && x.Activo == 1).FirstOrDefaultAsync();
            return cuestionario.CuestionarioId;
        }

        public async Task<List<RespuestaCuestionarioDetalle>> GetListRespuestas(int idRespuestaCuestionario)
        {
            var listRespuesta = await this.context.RespuestaCuestionarioDetalle.Where(x => x.RespuestaCuestionarioId == idRespuestaCuestionario)
                                                                              .Select(x => new RespuestaCuestionarioDetalle
                                                                              {
                                                                                  RespuestaId = x.RespuestaId
                                                                              }).ToListAsync();
            return listRespuesta;
                        
        }
    }
}

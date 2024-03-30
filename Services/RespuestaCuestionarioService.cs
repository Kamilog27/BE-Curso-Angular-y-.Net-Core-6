using BackEndCuestionario.Domain.IRepositories;
using BackEndCuestionario.Domain.IServices;
using BackEndCuestionario.Domain.Models;

namespace BackEndCuestionario.Services
{
    public class RespuestaCuestionarioService:IRespuestaCuestionarioService
    {
        private readonly IRespuestaCuestionarioRepository respuestaCuestionarioRepository;

        public RespuestaCuestionarioService(IRespuestaCuestionarioRepository respuestaCuestionarioRepository)
        {
            this.respuestaCuestionarioRepository = respuestaCuestionarioRepository;
        }


        public async Task SaveRespuestaCuestionario(RespuestaCuestionario respuestaCuestionario)
        {
             await this.respuestaCuestionarioRepository.SaveRespuestaCuestionario(respuestaCuestionario);
        }
        public async Task<List<RespuestaCuestionario>> ListRespuestaCuestionario(int idCuestionario,int idUsuario)
        {
            return await this.respuestaCuestionarioRepository.ListRespuestaCuestionario(idCuestionario, idUsuario);
        }

        public async Task<RespuestaCuestionario> BuscarRespuestaCuestionario(int idRtaCuestionario, int idUsuario)
        {
            return await respuestaCuestionarioRepository.BuscarRespuestaCuestionario(idRtaCuestionario, idUsuario);
        }

        public async Task EliminarRespuestaCuestionario(RespuestaCuestionario respuestaCuestionario)
        {
             await this.respuestaCuestionarioRepository.EliminarRespuestaCuestionario(respuestaCuestionario);
        }

        public async Task<int> GetIdCuestionarioByIdRespuesta(int idRespuestaCuestionario)
        {
            return await this.respuestaCuestionarioRepository.GetIdCuestionarioByIdRespuesta(idRespuestaCuestionario);
        }

        public async Task<List<RespuestaCuestionarioDetalle>> GetListRespuestas(int idRespuestaCuestionario)
        {
            return await this.respuestaCuestionarioRepository.GetListRespuestas(idRespuestaCuestionario);
        }
    }
}

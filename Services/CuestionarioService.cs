using BackEndCuestionario.Domain.IRepositories;
using BackEndCuestionario.Domain.IServices;
using BackEndCuestionario.Domain.Models;

namespace BackEndCuestionario.Services
{
    public class CuestionarioService:ICuestionarioService
    {
        private readonly ICuestionarioRepository cuestionarioRepository;

        public CuestionarioService(ICuestionarioRepository cuestionarioRepository)
        {
            this.cuestionarioRepository = cuestionarioRepository;
        }

        public async Task CreateCuestionario(Cuestionario cuestionario)
        {

            await this.cuestionarioRepository.CreateCuestionario(cuestionario);
        }

  

        public async Task<List<Cuestionario>> GetListCuestionarioByUser(int idUsuario)
        {
           return  await this.cuestionarioRepository.GetListCuestionarioByUser(idUsuario);
        }
        public async Task<Cuestionario> GetListCuestionario(int idCuestionario)
        {
            return await this.cuestionarioRepository.GetListCuestionario(idCuestionario);
        }

        public async Task<Cuestionario> BuscarCuestionario(int idCuestionario, int idUsuario)
        {
            return await this.cuestionarioRepository.BuscarCuestionario(idCuestionario,idUsuario);

        }

        public async Task EliminarCuestionario(Cuestionario cuestionario)
        {
            await this.cuestionarioRepository.EliminarCuestionario(cuestionario);
        }

        public async Task<List<Cuestionario>> GetListCuestionarios()
        {
            return await cuestionarioRepository.GetListCuestionarios();
        }
    }
}

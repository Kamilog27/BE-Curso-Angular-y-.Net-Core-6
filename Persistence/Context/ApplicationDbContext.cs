using BackEndCuestionario.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BackEndCuestionario.Persistence.Context
{
    public class ApplicationDbContext:DbContext
    {
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Pregunta> Pregunta { get; set; }
        public DbSet<Cuestionario> Cuestionario { get; set; }
        public DbSet<Respuesta> Respuesta { get; set; }
        public DbSet<RespuestaCuestionario> RespuestaCuestionario { get; set; }
        public DbSet<RespuestaCuestionarioDetalle> RespuestaCuestionarioDetalle { get; set; }



        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base (options)
        {
            
        }
    }
}

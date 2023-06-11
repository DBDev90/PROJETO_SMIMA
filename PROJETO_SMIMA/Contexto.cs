using Microsoft.EntityFrameworkCore;
using PROJETO_SMIMA.Entidades;

namespace PROJETO_SMIMA
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> opt):base(opt) { }

        public DbSet<Produto> PRODUTOS { get; set; }
        public DbSet<Usuario> USUARIOS { get; set; }
    }
}

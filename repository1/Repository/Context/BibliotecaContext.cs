using Microsoft.EntityFrameworkCore;
using Repository.Models;

namespace Repository.Context
{
    public class BibliotecaContext : DbContext
    {
        public BibliotecaContext(DbContextOptions<BibliotecaContext> op ): base(op){}

        public DbSet<Autor> Autor { get; set; }
        public DbSet<Categoria> Categoria { get; set; }
    }
}

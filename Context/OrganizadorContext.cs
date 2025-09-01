using Microsoft.EntityFrameworkCore;
using TrilhaApiDesafio.Models;

namespace TrilhaApiDesafio.Context
{
    public class OrganizadorContext : DbContext
    {
        public OrganizadorContext(DbContextOptions<OrganizadorContext> options) : base(options)
        {
            
        }

        public DbSet<Tarefa> Tarefas { get; set; }

        // Adicione este método para configurar a chave primária
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tarefa>()
                .HasKey(t => t.Id); // Define Id como Chave Primária
            
            modelBuilder.Entity<Tarefa>()
                .Property(t => t.Id)
                .ValueGeneratedOnAdd(); // Configura o Id para ser auto-incrementado
        }
    }
}











// using Microsoft.EntityFrameworkCore;
// using TrilhaApiDesafio.Models;

// namespace TrilhaApiDesafio.Context
// {
//     public class OrganizadorContext : DbContext
//     {
//         public OrganizadorContext(DbContextOptions<OrganizadorContext> options) : base(options)
//         {
            
//         }

//         public DbSet<Tarefa> Tarefas { get; set; }
//     }
// }
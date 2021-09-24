using Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PL.Context
{
    public class SecondHandContext : IdentityDbContext<ApplicationUser>
    {
        public SecondHandContext() : base()
        {
        }

        public SecondHandContext(DbContextOptions<SecondHandContext> options) : base(options)                                                    
        {
        }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<ApplicationUser> Usuarios { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Imagem> Imagem { get; set; }
        public DbSet<Pergunta> Perguntas { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=SecondHand;Trusted_Connection=True;");
        }

        //associar a PK do identity
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>().ToTable("AspNetUsers").HasKey(t => t.Id);
            base.OnModelCreating(builder);
        }
    }
}

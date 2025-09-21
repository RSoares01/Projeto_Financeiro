using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Projeto_Financeiro.Domain.Entities;

namespace Projeto_Financeiro.Infrastructure.Context
{
    public class FinContext : DbContext
    {
        public FinContext(DbContextOptions<FinContext> options) : base(options) { }

        public DbSet<Categorias> Categorias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Categorias>(entity =>
            {
                entity.ToTable("Categorias");
                entity.HasKey(c => c.Id);
            });
        }
    }
}

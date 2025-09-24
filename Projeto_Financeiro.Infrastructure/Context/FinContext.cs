using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Projeto_Financeiro.Domain.Entities;
using Projeto_Financeiro.Domain.ReadModel;

namespace Projeto_Financeiro.Infrastructure.Context
{
    public class FinContext : DbContext
    {
        public FinContext(DbContextOptions<FinContext> options) : base(options) { }

        public DbSet<Categorias> Categorias { get; set; }
        public DbSet<Transacoes> Transacoes { get; set; }

        public DbSet<ResumoFinanceiro> ResumoFinanceiro { get; set; }
        public DbSet<RelatorioCategoria> RelatorioCategoria { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            base.OnModelCreating(modelBuilder);

            //Tabelas 
            modelBuilder.Entity<Categorias>().ToTable("Categorias");
            modelBuilder.Entity<Transacoes>().ToTable("Transacoes");

            //Chaves primárias
            modelBuilder.Entity<Categorias>().HasKey(c => c.Id);
            modelBuilder.Entity<Transacoes>().HasKey(t => t.Id);

            //Não tem PK
            modelBuilder.Entity<ResumoFinanceiro>().HasNoKey();
            modelBuilder.Entity<RelatorioCategoria>().HasNoKey();


            //Chaves estrangeiras 
            modelBuilder.Entity<Transacoes>()
                .HasOne(c => c.Categoria)
                .WithMany()
                .HasForeignKey(c => c.CategoriaId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

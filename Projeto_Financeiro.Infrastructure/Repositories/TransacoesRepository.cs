using Microsoft.EntityFrameworkCore;
using Projeto_Financeiro.Domain.Entities;
using Projeto_Financeiro.Domain.Interfaces.IRepositories;
using Projeto_Financeiro.Infrastructure.Context;

namespace Projeto_Financeiro.Infrastructure.Repositories
{
    public class TransacoesRepository : ITransacoesRepository
    {
        private readonly FinContext _context;

        public FinContext Context => _context;

        public TransacoesRepository(FinContext context)
        {
            _context = context;
        }

        public async Task<Transacoes?> GetByIdAsync(int id)
        {
            return await Context.Transacoes
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<Transacoes>> GetAllAsync()
        {
            return await Context.Transacoes.ToListAsync();
        }

        public async Task CreateAsync(Transacoes transacoes)
        {
            await Context.Transacoes.AddAsync(transacoes);
            await Context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Transacoes transacoes)
        {
            Context.Transacoes.Update(transacoes);
            await Context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await Context.Transacoes.FindAsync(id);
            if (entity is null) return;

            Context.Transacoes.Remove(entity);
            await Context.SaveChangesAsync();
        }

    }
}

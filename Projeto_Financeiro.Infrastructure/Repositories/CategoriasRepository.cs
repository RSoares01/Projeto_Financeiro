using Microsoft.EntityFrameworkCore;
using Projeto_Financeiro.Domain.Entities;
using Projeto_Financeiro.Domain.Interfaces.IRepositories;
using Projeto_Financeiro.Infrastructure.Context;

namespace Projeto_Financeiro.Infrastructure.Repositories
{
    public class CategoriasRepository : ICategoriasRepository
    {
        private readonly FinContext _context;

        public FinContext Context => _context;

        public CategoriasRepository(FinContext context) 
        { 
            _context = context;
        }

        public async Task<Categorias?> GetByIdAsync(int id)
        {
            return await Context.Categorias
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Categorias>> GetAllAsync()
        {
            return await Context.Categorias.ToListAsync();
        }

        public async Task CreateAsync(Categorias categorias)
        {
            await Context.Categorias.AddAsync(categorias);
            await Context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Categorias categorias)
        {
            Context.Categorias.Update(categorias);
            await Context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var categoria = await Context.Categorias.FirstOrDefaultAsync(c => c.Id == id);
            if (categoria != null)
            {
               Context.RemoveRange(categoria);
            }
        }
    }
}

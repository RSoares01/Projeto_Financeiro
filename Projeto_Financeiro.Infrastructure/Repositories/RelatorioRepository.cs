using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Projeto_Financeiro.Application.DTOs;
using Projeto_Financeiro.Domain.Interfaces.IRepositories;
using Projeto_Financeiro.Domain.ReadModel;
using Projeto_Financeiro.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Financeiro.Infrastructure.Repositories
{
    public class RelatorioRepository : IRelatorioRepository
    {
        private readonly FinContext _context;

        public RelatorioRepository(FinContext context)
        {
            _context = context;
        }

        public async Task<ResumoFinanceiro> ObterResumoAsync(DateTime dataInicio, DateTime dataFim)
        {
            var sql = "EXEC usp_RelatorioResumo @DataInicio, @DataFim";

            var dataInicioParam = new SqlParameter("@DataInicio", dataInicio);
            var dataFimParam = new SqlParameter("@DataFim", dataFim);

            var resultado = await _context.ResumoFinanceiro
                .FromSqlRaw(sql, dataInicioParam, dataFimParam)
                .AsNoTracking()
                .ToListAsync();

            return resultado.FirstOrDefault(); // Sempre retorna uma linha
        }
    }
}

using Data.Context;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class VendaRepository : IVendaRepository
    {
        private readonly VendaDbContext _context;

        public VendaRepository(VendaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Venda>> GetAllAsync()
        {
            return await _context.Vendas.Include(v => v.Itens).ToListAsync();
        }

        public async Task<Venda> GetByIdAsync(Guid id)
        {
            return await _context.Vendas.Include(v => v.Itens).FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task AddAsync(Venda venda)
        {
            await _context.Vendas.AddAsync(venda);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Venda venda)
        {
            _context.Vendas.Update(venda);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Venda venda)
        {
            venda.Cancelar();
            _context.Vendas.Update(venda);
            await _context.SaveChangesAsync();
        }
    }
}

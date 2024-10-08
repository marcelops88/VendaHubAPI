﻿using Data.Context;
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

        public async Task<Venda> GetByNumeroCompraAsync(int numeroVenda)
        {
            return await _context.Vendas.Include(v => v.Itens).FirstOrDefaultAsync(v => v.NumeroVenda == numeroVenda);
        }

        public async Task<Venda> AddAsync(Venda venda)
        {
            await _context.Vendas.AddAsync(venda);
            await _context.SaveChangesAsync();
            return venda;
        }

        public async Task<Venda> UpdateAsync(Venda venda)
        {
            _context.Vendas.Update(venda);
            await _context.SaveChangesAsync();
            return venda;
        }

        public async Task DeleteAsync(Venda venda)
        {
            _context.Vendas.Update(venda);
            await _context.SaveChangesAsync();
        }
    }
}

using Domain.Entities;
using Domain.Interfaces;

namespace Domain.Services
{
    public class VendaService : IVendaService
    {
        public decimal CalcularValorTotal(Venda venda)
        {
            throw new NotImplementedException();
        }

        public void CancelarItemVenda(ItemVenda item)
        {
            throw new NotImplementedException();
        }

        public Task<Venda> CreateVendaAsync(Venda venda)
        {
            throw new NotImplementedException();
        }

        public Task DeleteVendaAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Venda>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Venda> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Venda> UpdateVendaAsync(Guid id, Venda venda)
        {
            throw new NotImplementedException();
        }
    }
}

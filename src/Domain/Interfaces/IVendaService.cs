using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IVendaService
    {
        Task<Venda> CreateVendaAsync(Venda venda);
        Task<Venda> UpdateVendaAsync(Guid id, Venda venda);
        Task DeleteVendaAsync(Guid id);
        decimal CalcularValorTotal(Venda venda);
        void CancelarItemVenda(ItemVenda item);
    }
}

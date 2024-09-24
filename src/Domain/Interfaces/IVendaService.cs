using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IVendaService
    {
        Task<Venda> CreateVendaAsync(Venda venda);
        Task<Venda> UpdateVendaAsync(int numeroVenda, Venda venda);
        Task DeleteVendaAsync(int numeroVenda);
        Task<IEnumerable<Venda>> GetAllAsync();
        Task<Venda> GetByNumeroCompraAsync(int numeroVenda);
        decimal CalcularValorTotal(Venda venda);
        void CancelarItemVenda(ItemVenda item);
    }
}

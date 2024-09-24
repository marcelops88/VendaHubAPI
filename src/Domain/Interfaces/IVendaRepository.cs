using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IVendaRepository
    {
        Task<IEnumerable<Venda>> GetAllAsync();
        Task<Venda> GetByNumeroCompraAsync(int numeroVenda);
        Task<Venda> AddAsync(Venda venda);
        Task<Venda> UpdateAsync(Venda venda);
        Task DeleteAsync(Venda venda);
    }
}

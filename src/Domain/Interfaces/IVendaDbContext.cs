using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domain.Interfaces
{
    public interface IVendaDbContext
    {
        DbSet<Venda> Vendas { get; }
        DbSet<ItemVenda> ItensVenda { get; }
    }
}

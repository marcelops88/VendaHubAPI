using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Domain.Services
{
    public class VendaService : IVendaService
    {
        private readonly IVendaRepository _vendaRepository;
        private readonly ILogger<VendaService> _logger;

        public VendaService(IVendaRepository vendaRepository, ILogger<VendaService> logger)
        {
            _vendaRepository = vendaRepository;
            _logger = logger;
        }

        public async Task<Venda> CreateVendaAsync(Venda venda)
        {
            var vendaEncontrada = await GetByNumeroCompraAsync(venda.NumeroVenda);
            if (vendaEncontrada != null)
            {
                throw new InvalidOperationException("Já existe uma venda criada com esse numero!");
            }

            var vendaCriada = await _vendaRepository.AddAsync(venda);

            _logger.LogInformation("CompraCriada: {@Venda}", vendaCriada);

            return vendaCriada;
        }
        public async Task<Venda> UpdateVendaAsync(int numeroVenda, Venda venda)
        {
            var vendaEncontrada = await GetByNumeroCompraAsync(numeroVenda);
            if (vendaEncontrada == null)
            {
                throw new KeyNotFoundException("Venda não encontrada.");
            }

            vendaEncontrada.AtualizarVenda(
                venda.NumeroVenda,
                venda.NomeCliente,
                venda.Filial,
                venda.Itens,
                venda.CpfCliente,
                venda.TelefoneCliente,
                venda.EmailCliente,
                venda.Cancelado
            );

            await _vendaRepository.UpdateAsync(vendaEncontrada);

            _logger.LogInformation("CompraAlterada: {@Venda}", vendaEncontrada);

            return vendaEncontrada;
        }

        public async Task<Venda> DeleteVendaAsync(int numeroVenda)
        {
            var venda = await GetByNumeroCompraAsync(numeroVenda);
            if (venda == null)
            {
                throw new KeyNotFoundException("Venda não encontrada.");
            }

            venda.Cancelar();

            await _vendaRepository.UpdateAsync(venda);

            _logger.LogInformation("CompraCancelada: {@Venda}", venda);

            return venda;
        }

        public Task<IEnumerable<Venda>> GetAllAsync()
        {
            return _vendaRepository.GetAllAsync();
        }

        public Task<Venda> GetByNumeroCompraAsync(int numeroVenda)
        {
            return _vendaRepository.GetByNumeroCompraAsync(numeroVenda);
        }

    }
}

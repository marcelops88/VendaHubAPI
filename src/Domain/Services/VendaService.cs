using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Domain.Services
{
    public class VendaService : IVendaService
    {
        private readonly IVendaRepository _vendaRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<VendaService> _logger;

        public VendaService(IVendaRepository vendaRepository, IMapper mapper, ILogger<VendaService> logger)
        {
            _vendaRepository = vendaRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Venda> CreateVendaAsync(Venda venda)
        {
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

            var input = _mapper.Map<Venda>(venda);

            vendaEncontrada.AtualizarVenda(
                input.NumeroVenda,
                input.NomeCliente,
                input.Filial,
                input.Itens,
                input.CpfCliente,
                input.TelefoneCliente,
                input.EmailCliente,
                input.Cancelado
            );

            await _vendaRepository.UpdateAsync(vendaEncontrada);

            _logger.LogInformation("CompraAlterada: {@Venda}", vendaEncontrada);

            return vendaEncontrada;
        }

        public Task DeleteVendaAsync(int numeroVenda)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Venda>> GetAllAsync()
        {
            return _vendaRepository.GetAllAsync();
        }

        public Task<Venda> GetByNumeroCompraAsync(int numeroVenda)
        {
            return _vendaRepository.GetByNumeroCompraAsync(numeroVenda);
        }

        public decimal CalcularValorTotal(Venda venda)
        {
            throw new NotImplementedException();
        }

        public void CancelarItemVenda(ItemVenda item)
        {
            throw new NotImplementedException();
        }
    }
}

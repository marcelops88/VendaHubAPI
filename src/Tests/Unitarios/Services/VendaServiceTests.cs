using Domain.Entities;
using Domain.Interfaces;
using Domain.Services;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace Tests.Unitarios.Services
{
    public class VendaServiceTests
    {
        private readonly IVendaRepository _vendaRepository;
        private readonly ILogger<VendaService> _logger;
        private readonly VendaService _vendaService;

        public VendaServiceTests()
        {
            _vendaRepository = Substitute.For<IVendaRepository>();
            _logger = Substitute.For<ILogger<VendaService>>();
            _vendaService = new VendaService(_vendaRepository, _logger);
        }

        [Fact]
        public async Task CreateVendaAsync_ShouldCreateVenda_WhenVendaDoesNotExist()
        {
            // Arrange
            var venda = new Venda(1, "Cliente Teste", "Filial Teste", new List<ItemVenda>(), "123.456.789-00", "(11) 99999-9999", "teste@example.com");
            _vendaRepository.GetByNumeroCompraAsync(venda.NumeroVenda).Returns(Task.FromResult<Venda>(null));
            _vendaRepository.AddAsync(Arg.Any<Venda>()).Returns(Task.FromResult(venda));

            // Act
            var result = await _vendaService.CreateVendaAsync(venda);

            // Assert
            result.Should().BeEquivalentTo(venda);
            await _vendaRepository.Received(1).AddAsync(venda);
        }

        [Fact]
        public async Task CreateVendaAsync_ShouldThrowInvalidOperationException_WhenVendaExists()
        {
            // Arrange
            var venda = new Venda(1, "Cliente Teste", "Filial Teste", new List<ItemVenda>(), "123.456.789-00", "(11) 99999-9999", "teste@example.com");
            _vendaRepository.GetByNumeroCompraAsync(venda.NumeroVenda).Returns(Task.FromResult(venda));

            // Act
            Func<Task> act = async () => await _vendaService.CreateVendaAsync(venda);

            // Assert
            await act.Should().ThrowAsync<InvalidOperationException>()
                .WithMessage("Já existe uma venda criada com esse numero!");
        }

        [Fact]
        public async Task UpdateVendaAsync_ShouldUpdateVenda_WhenVendaExists()
        {
            // Arrange
            var vendaExistente = new Venda(1, "Cliente Antigo", "Filial Antiga", new List<ItemVenda>(), "123.456.789-00", "(11) 99999-9999", "teste@example.com");
            var vendaAtualizada = new Venda(1, "Cliente Atualizado", "Filial Atualizada", new List<ItemVenda>(), "123.456.789-00", "(11) 99999-9999", "teste.novo@example.com");
            _vendaRepository.GetByNumeroCompraAsync(vendaExistente.NumeroVenda).Returns(Task.FromResult(vendaExistente));

            // Act
            var result = await _vendaService.UpdateVendaAsync(vendaExistente.NumeroVenda, vendaAtualizada);

            // Assert
            result.Should().BeEquivalentTo(vendaAtualizada, options => options
                .Excluding(v => v.DataVenda)
                .Excluding(v => v.Id)
                .Excluding(v => v.DataCriacao)
                .Excluding(v => v.DataAtualizacao)
            );

            await _vendaRepository.Received(1).UpdateAsync(Arg.Is<Venda>(v => v.NomeCliente == "Cliente Atualizado"));
        }

        [Fact]
        public async Task UpdateVendaAsync_ShouldThrowKeyNotFoundException_WhenVendaDoesNotExist()
        {
            // Arrange
            var vendaAtualizada = new Venda(1, "Cliente Atualizado", "Filial Atualizada", new List<ItemVenda>(), "123.456.789-00", "(11) 99999-9999", "teste.novo@example.com");
            _vendaRepository.GetByNumeroCompraAsync(vendaAtualizada.NumeroVenda).Returns(Task.FromResult<Venda>(null));

            // Act
            Func<Task> act = async () => await _vendaService.UpdateVendaAsync(vendaAtualizada.NumeroVenda, vendaAtualizada);

            // Assert
            await act.Should().ThrowAsync<KeyNotFoundException>()
                .WithMessage("Venda não encontrada.");
        }

        [Fact]
        public async Task DeleteVendaAsync_ShouldDeleteVenda_WhenVendaExists()
        {
            // Arrange
            var venda = new Venda(1, "Cliente Teste", "Filial Teste", new List<ItemVenda>(), "123.456.789-00", "(11) 99999-9999", "teste@example.com");
            await _vendaRepository.AddAsync(venda);

            _vendaRepository.GetByNumeroCompraAsync(venda.NumeroVenda).Returns(Task.FromResult(venda));

            // Act
            var result = await _vendaService.DeleteVendaAsync(venda.NumeroVenda);

            // Assert
            result.Should().BeEquivalentTo(venda);
            result.Cancelado.Should().BeTrue();
            await _vendaRepository.Received(1).UpdateAsync(venda);
        }


        [Fact]
        public async Task DeleteVendaAsync_ShouldThrowKeyNotFoundException_WhenVendaDoesNotExist()
        {
            // Arrange
            _vendaRepository.GetByNumeroCompraAsync(1).Returns(Task.FromResult<Venda>(null));

            // Act
            Func<Task> act = async () => await _vendaService.DeleteVendaAsync(1);

            // Assert
            await act.Should().ThrowAsync<KeyNotFoundException>()
                .WithMessage("Venda não encontrada.");
        }
    }
}

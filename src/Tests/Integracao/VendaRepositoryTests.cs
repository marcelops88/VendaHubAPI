using Bogus;
using Data.Context;
using Data.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Tests.Integracao
{
    public class VendaRepositoryTests
    {
        private VendaDbContext _context;
        private VendaRepository _repository;
        private Faker<Venda> _vendaFaker;
        private Faker<ItemVenda> _itemVendaFaker;

        public VendaRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<VendaDbContext>()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                 .Options;

            _context = new VendaDbContext(options);
            _repository = new VendaRepository(_context);

            _itemVendaFaker = new Faker<ItemVenda>()
                .RuleFor(i => i.NomeProduto, f => f.Commerce.ProductName())
                .RuleFor(i => i.Quantidade, f => f.Random.Int(1, 10))
                .RuleFor(i => i.ValorUnitario, f => f.Random.Decimal(500, 2000));

            _vendaFaker = new Faker<Venda>()
                .CustomInstantiator(f => new Venda(f.Random.Int(1, 1000), f.Person.FullName, f.Company.CompanyName(),
                    _itemVendaFaker.Generate(3), "088.713.851.97", f.Phone.PhoneNumber(), f.Internet.Email()));
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Fact]
        public async Task AddAsync_ShouldAddVendaSuccessfully()
        {
            // Arrange
            var venda = _vendaFaker.Generate();

            // Act
            var result = await _repository.AddAsync(venda);

            // Assert
            var savedVenda = await _context.Vendas.FirstOrDefaultAsync(v => v.NumeroVenda == venda.NumeroVenda);
            Assert.NotNull(savedVenda);
            Assert.Equal(venda.NumeroVenda, savedVenda.NumeroVenda);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllVendas()
        {
            // Arrange
            var venda1 = _vendaFaker.Generate();
            var venda2 = _vendaFaker.Generate();

            await _repository.AddAsync(venda1);
            await _repository.AddAsync(venda2);

            // Act
            var result = await _repository.GetAllAsync();

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Equal(2, await _context.Vendas.CountAsync());
        }

        [Fact]
        public async Task GetByNumeroCompraAsync_ShouldReturnVendaByNumero()
        {
            // Arrange
            var venda = _vendaFaker.Generate();
            await _repository.AddAsync(venda);

            // Act
            var result = await _repository.GetByNumeroCompraAsync(venda.NumeroVenda);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(venda.NumeroVenda, result.NumeroVenda);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateVenda()
        {
            // Arrange
            var venda = _vendaFaker.Generate();
            await _repository.AddAsync(venda);

            var itensVenda = new List<ItemVenda>();
            string novoNomeCliente = "João Silva Atualizado";
            string novaFilial = "Filial Alterada";
            string novoCpfCliente = "123.456.789-00";
            string novoTelefoneCliente = "(11) 98765-4321";
            string novoEmailCliente = "joao.silva@email.com";
            bool novoCancelado = false;

            // Act
            venda.AtualizarVenda(novoNomeCliente, novaFilial, itensVenda, novoCpfCliente, novoTelefoneCliente, novoEmailCliente, novoCancelado);
            await _repository.UpdateAsync(venda);

            // Assert
            var updatedVenda = await _repository.GetByNumeroCompraAsync(venda.NumeroVenda);
            Assert.Equal(novaFilial, updatedVenda.Filial);
        }

        [Fact]
        public async Task DeleteAsync_ShouldPerformLogicalDeletion()
        {
            // Arrange
            var venda = _vendaFaker.Generate();
            await _repository.AddAsync(venda);

            // Act
            venda.Cancelar();
            await _repository.DeleteAsync(venda);

            // Assert
            var deletedVenda = await _repository.GetByNumeroCompraAsync(venda.NumeroVenda);
            Assert.True(deletedVenda.Cancelado);
        }
    }

}

using API.DTOs.Requests;
using API.DTOs.Responses;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace Tests.Unitarios.Controllers
{
    public class VendaControllerTests
    {
        private readonly IVendaService _vendaService;
        private readonly IMapper _mapper;
        private readonly VendaController _controller;

        public VendaControllerTests()
        {
            _vendaService = Substitute.For<IVendaService>();
            _mapper = Substitute.For<IMapper>();
            _controller = new VendaController(_vendaService, _mapper);
        }

        [Fact]
        public async Task GetAll_ShouldReturnOk_WhenVendasExist()
        {
            // Arrange
            var itensVenda = new List<ItemVenda>();
            var vendas = new List<Venda>
              {
                        new Venda(1, "João Silva", "Filial Centro", itensVenda, "123.456.789-00", "(11) 98765-4321", "joao.silva@example.com")
              };

            var vendasResponse = new List<VendaResponse> { new VendaResponse { NumeroVenda = 1 } };

            _vendaService.GetAllAsync().Returns(vendas);
            _mapper.Map<IEnumerable<VendaResponse>>(vendas).Returns(vendasResponse);

            // Act
            var result = await _controller.GetAll();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(vendasResponse);
        }


        [Fact]
        public async Task GetById_ShouldReturnOk_WhenVendaExists()
        {
            // Arrange
            var itensVenda = new List<ItemVenda>();
            var venda = new Venda(1, "João Silva", "Filial Centro", itensVenda, "123.456.789-00", "(11) 98765-4321", "joao.silva@example.com");
            var vendaResponse = new VendaResponse { NumeroVenda = 1 };
            _vendaService.GetByNumeroCompraAsync(1).Returns(venda);
            _mapper.Map<VendaResponse>(venda).Returns(vendaResponse);

            // Act
            var result = await _controller.GetById(1);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(vendaResponse);
        }

        [Fact]
        public async Task GetById_ShouldReturnNotFound_WhenVendaDoesNotExist()
        {
            // Arrange
            _vendaService.GetByNumeroCompraAsync(1).Returns((Venda)null);

            // Act
            var result = await _controller.GetById(1);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task Create_ShouldReturnCreated_WhenVendaIsCreated()
        {
            // Arrange
            var request = new VendaRequest();
            var itensVenda = new List<ItemVenda>();
            var venda = new Venda(1, "João Silva", "Filial Centro", itensVenda, "123.456.789-00", "(11) 98765-4321", "joao.silva@example.com");
            var vendaResponse = new VendaResponse { NumeroVenda = 1 };
            _mapper.Map<Venda>(request).Returns(venda);
            _vendaService.CreateVendaAsync(venda).Returns(venda);
            _mapper.Map<VendaResponse>(venda).Returns(vendaResponse);

            // Act
            var result = await _controller.Create(request);

            // Assert
            result.Should().BeOfType<CreatedAtActionResult>();
            var createdAtActionResult = result as CreatedAtActionResult;
            createdAtActionResult.Value.Should().BeEquivalentTo(vendaResponse);
        }

        [Fact]
        public async Task Update_ShouldReturnOk_WhenVendaIsUpdated()
        {
            // Arrange
            var request = new VendaRequest();
            var itensVenda = new List<ItemVenda>();
            var venda = new Venda(1, "João Silva", "Filial Centro", itensVenda, "123.456.789-00", "(11) 98765-4321", "joao.silva@example.com");
            var vendaResponse = new VendaResponse { NumeroVenda = 1 };
            _mapper.Map<Venda>(request).Returns(venda);
            _vendaService.UpdateVendaAsync(1, venda).Returns(venda);
            _mapper.Map<VendaResponse>(venda).Returns(vendaResponse);

            // Act
            var result = await _controller.Update(1, request);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(vendaResponse);
        }

        [Fact]
        public async Task Update_ShouldReturnNotFound_WhenVendaDoesNotExist()
        {
            // Arrange
            var request = new VendaRequest();
            var itensVenda = new List<ItemVenda>();
            var venda = new Venda(1, "João Silva", "Filial Centro", itensVenda, "123.456.789-00", "(11) 98765-4321", "joao.silva@example.com");
            _mapper.Map<Venda>(request).Returns(venda);
            _vendaService.UpdateVendaAsync(1, venda).Returns(Task.FromException<Venda>(new KeyNotFoundException()));

            // Act
            var result = await _controller.Update(1, request);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task Delete_ShouldReturnNoContent_WhenVendaIsDeleted()
        {
            // Arrange
            var venda = new Venda(1, "João Silva", "Filial Centro", new List<ItemVenda>(), "123.456.789-00", "(11) 98765-4321", "joao.silva@example.com");
            _vendaService.DeleteVendaAsync(1).Returns(venda);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }


        [Fact]
        public async Task Delete_ShouldReturnNotFound_WhenVendaDoesNotExist()
        {
            // Arrange
            _vendaService.DeleteVendaAsync(1).Returns(Task.FromException<Venda>(new KeyNotFoundException()));

            // Act
            var result = await _controller.Delete(1);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

    }
}

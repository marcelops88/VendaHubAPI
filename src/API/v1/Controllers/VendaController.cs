using API.Configurations.Attributes;
using API.DTOs.Requests;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.v1.Controllers
{
    [ApiController]
    [ApiV1Route("[controller]")]
    [Produces("application/json")]
    public class VendaController : ControllerBase
    {
        private readonly IVendaRepository _vendaRepository;
        private readonly IMapper _mapper;

        public VendaController(IVendaRepository vendaRepository)
        {
            _vendaRepository = vendaRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var vendas = await _vendaRepository.GetAllAsync();
            return Ok(vendas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var venda = await _vendaRepository.GetByIdAsync(id);
            if (venda == null)
            {
                return NotFound();
            }
            return Ok(venda);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] VendaRequest request)
        {
            var input = _mapper.Map<Venda>(request);
            var venda = await _vendaRepository.AddAsync(input);
            return CreatedAtAction(nameof(GetById), new { id = venda.Id }, venda);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] VendaRequest request)
        {
            var venda = await _vendaRepository.GetByIdAsync(id);
            if (venda == null)
            {
                return NotFound();
            }
            var input = _mapper.Map<Venda>(request);

            venda.AtualizarVenda(
                   input.NumeroVenda,
                   input.NomeCliente,
                   input.Filial,
                   input.Itens,
                   input.CpfCliente,
                   input.TelefoneCliente,
                   input.EmailCliente
               );

            await _vendaRepository.UpdateAsync(venda);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var venda = await _vendaRepository.GetByIdAsync(id);
            if (venda == null)
            {
                return NotFound();
            }

            await _vendaRepository.DeleteAsync(venda);
            return NoContent();
        }
    }

}

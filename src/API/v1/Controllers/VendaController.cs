using API.Configurations.Attributes;
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
        public async Task<IActionResult> Create([FromBody] Venda venda)
        {
            await _vendaRepository.AddAsync(venda);
            return CreatedAtAction(nameof(GetById), new { id = venda.Id }, venda);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Venda vendaAtualizada)
        {
            var venda = await _vendaRepository.GetByIdAsync(id);
            if (venda == null)
            {
                return NotFound();
            }

            venda.AtualizarVenda(
                   vendaAtualizada.NumeroVenda,
                   vendaAtualizada.NomeCliente,
                   vendaAtualizada.Filial,
                   vendaAtualizada.Itens,
                   vendaAtualizada.CpfCliente,
                   vendaAtualizada.TelefoneCliente,
                   vendaAtualizada.EmailCliente
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

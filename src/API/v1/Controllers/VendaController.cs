using API.Configurations.Attributes;
using API.DTOs.Requests;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[ApiV1Route("[controller]")]
[Produces("application/json")]
public class VendaController : ControllerBase
{
    private readonly IVendaService _vendaService;
    private readonly IMapper _mapper;
    public VendaController(IVendaService vendaService, IMapper mapper)
    {
        _vendaService = vendaService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var vendas = await _vendaService.GetAllAsync();
        return Ok(vendas);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var venda = await _vendaService.GetByIdAsync(id);
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
        var vendaCriada = await _vendaService.CreateVendaAsync(input);
        return CreatedAtAction(nameof(GetById), new { id = vendaCriada.Id }, vendaCriada);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] VendaRequest request)
    {
        try
        {
            var input = _mapper.Map<Venda>(request);
            var vendaAtualizada = await _vendaService.UpdateVendaAsync(id, input);
            return Ok(vendaAtualizada);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _vendaService.DeleteVendaAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}

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

    [HttpGet("{numeroVenda}")]
    public async Task<IActionResult> GetById(int numeroVenda)
    {
        var venda = await _vendaService.GetByNumeroCompraAsync(numeroVenda);
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
        return CreatedAtAction(nameof(GetById), new { numeroVenda = vendaCriada.NumeroVenda }, vendaCriada);
    }

    [HttpPut("{numeroVenda}")]
    public async Task<IActionResult> Update(int numeroVenda, [FromBody] VendaRequest request)
    {
        try
        {
            var input = _mapper.Map<Venda>(request);
            var vendaAtualizada = await _vendaService.UpdateVendaAsync(numeroVenda, input);
            return Ok(vendaAtualizada);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{numeroVenda}")]
    public async Task<IActionResult> Delete(int numeroVenda)
    {
        try
        {
            await _vendaService.DeleteVendaAsync(numeroVenda);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}

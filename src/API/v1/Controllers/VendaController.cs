using API.Configurations.Attributes;
using API.DTOs.Requests;
using API.DTOs.Responses;
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
    [ProducesResponseType(typeof(IEnumerable<VendaResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var vendas = await _vendaService.GetAllAsync();
        var vendasResponse = _mapper.Map<IEnumerable<VendaResponse>>(vendas);
        return Ok(vendasResponse);
    }

    [HttpGet("{numeroVenda}")]
    [ProducesResponseType(typeof(VendaResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int numeroVenda)
    {
        var venda = await _vendaService.GetByNumeroCompraAsync(numeroVenda);
        if (venda == null)
        {
            return NotFound();
        }
        var vendaResponse = _mapper.Map<VendaResponse>(venda);
        return Ok(vendaResponse);
    }

    [HttpPost]
    [ProducesResponseType(typeof(VendaResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] VendaRequest request)
    {
        var input = _mapper.Map<Venda>(request);
        var vendaCriada = await _vendaService.CreateVendaAsync(input);
        var vendaResponse = _mapper.Map<VendaResponse>(vendaCriada);
        return CreatedAtAction(nameof(GetById), new { numeroVenda = vendaCriada.NumeroVenda }, vendaResponse);
    }

    [HttpPut("{numeroVenda}")]
    [ProducesResponseType(typeof(VendaResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int numeroVenda, [FromBody] VendaRequest request)
    {
        try
        {
            var input = _mapper.Map<Venda>(request);
            var vendaAtualizada = await _vendaService.UpdateVendaAsync(numeroVenda, input);
            var vendaResponse = _mapper.Map<VendaResponse>(vendaAtualizada);
            return Ok(vendaResponse);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{numeroVenda}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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

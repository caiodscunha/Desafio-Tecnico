using DesafioTecnico.Api.DTOs;
using DesafioTecnico.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace DesafioTecnico.Api.Controllers;

/// <summary>
/// Endpoint HTTP para a consulta de totais (receitas, despesas e saldo por pessoa + geral).
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class TotaisController : ControllerBase
{
    private readonly ITotaisService _totaisService;

    public TotaisController(ITotaisService totaisService)
    {
        _totaisService = totaisService;
    }

    /// <summary>
    /// GET /api/totais
    /// Lista os totais de cada pessoa cadastrada, seguido do total geral.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<TotaisResponseDto>> Obter()
    {
        var totais = await _totaisService.ObterAsync();
        return Ok(totais);
    }
}

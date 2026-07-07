using DesafioTecnico.Api.DTOs;
using DesafioTecnico.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace DesafioTecnico.Api.Controllers;

/// <summary>
/// Endpoints HTTP para cadastro de transações.
/// [Route("api/[controller]")]: como a classe se chama "TransacoesController",
/// [controller] vira "Transacoes" -> rota final "api/transacoes".
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class TransacoesController : ControllerBase
{
    private readonly ITransacaoService _transacaoService;

    public TransacoesController(ITransacaoService transacaoService)
    {
        _transacaoService = transacaoService;
    }

    /// <summary>
    /// POST /api/transacoes
    /// Cria uma nova transação a partir dos dados enviados no corpo da requisição (JSON).
    /// Retorna 404 se a pessoa informada não existir, ou 400 se violar a regra de
    /// "menor de 18 anos só pode cadastrar despesa" (ver TransacaoService.CriarAsync).
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<TransacaoResponseDto>> Criar([FromBody] CreateTransacaoDto dto)
    {
        try
        {
            var transacaoCriada = await _transacaoService.CriarAsync(dto);

            // Retorna HTTP 201 (Created) com a transação criada no corpo da resposta.
            return CreatedAtAction(nameof(Listar), new { }, transacaoCriada);
        }
        catch (KeyNotFoundException ex)
        {
            // Pessoa informada não existe.
            return NotFound(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            // Regra de negócio violada (ex.: menor de idade tentando cadastrar receita).
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// GET /api/transacoes
    /// Lista todas as transações cadastradas.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TransacaoResponseDto>>> Listar()
    {
        var transacoes = await _transacaoService.ListarAsync();
        return Ok(transacoes);
    }
}

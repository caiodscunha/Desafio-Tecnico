using DesafioTecnico.Api.DTOs;
using DesafioTecnico.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace DesafioTecnico.Api.Controllers;

/// <summary>
/// Endpoints HTTP para cadastro de pessoas.
/// [ApiController] habilita validação automática dos DTOs (ex.: [Required] em CreatePessoaDto)
/// e outras conveniências padrão de API REST.
/// [Route] define o prefixo da URL: como a classe se chama "PessoasController",
/// [controller] vira "pessoas" -> rota final "api/pessoas".
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class PessoasController : ControllerBase
{
    private readonly IPessoaService _pessoaService;

    public PessoasController(IPessoaService pessoaService)
    {
        _pessoaService = pessoaService;
    }

    /// <summary>
    /// POST /api/pessoas
    /// Cria uma nova pessoa a partir dos dados enviados no corpo da requisição (JSON).
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<PessoaResponseDto>> Criar([FromBody] CreatePessoaDto dto)
    {
        var pessoaCriada = await _pessoaService.CriarAsync(dto);

        // Retorna HTTP 201 (Created) com a pessoa criada no corpo da resposta.
        return CreatedAtAction(nameof(Listar), new { }, pessoaCriada);
    }

    /// <summary>
    /// GET /api/pessoas
    /// Lista todas as pessoas cadastradas.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PessoaResponseDto>>> Listar()
    {
        var pessoas = await _pessoaService.ListarAsync();
        return Ok(pessoas);
    }
}

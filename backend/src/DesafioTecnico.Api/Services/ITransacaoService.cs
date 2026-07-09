using DesafioTecnico.Api.DTOs;

namespace DesafioTecnico.Api.Services;

/// <summary>
/// Interface (contrato) do serviço de transações.
/// O Controller depende dessa interface, não da implementação concreta (TransacaoService) —
/// mesma ideia da IPessoaService.
/// </summary>
public interface ITransacaoService
{
    Task<TransacaoResponseDto> CriarAsync(CreateTransacaoDto dto);
    Task<IEnumerable<TransacaoResponseDto>> ListarAsync();
}

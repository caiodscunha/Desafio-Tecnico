using DesafioTecnico.Api.DTOs;

namespace DesafioTecnico.Api.Services;

/// <summary>
/// Interface (contrato) do serviço de totais.
/// O Controller depende dessa interface, não da implementação concreta (TotaisService) —
/// mesma ideia da IPessoaService e ITransacaoService.
/// </summary>
public interface ITotaisService
{
    Task<TotaisResponseDto> ObterAsync();
}

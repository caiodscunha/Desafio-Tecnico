using DesafioTecnico.Api.DTOs;

namespace DesafioTecnico.Api.Services;

/// <summary>
/// Interface (contrato) do serviço de pessoas.
/// O Controller depende dessa interface, não da implementação concreta (PessoaService).
/// Isso é "injeção de dependência": facilita trocar a implementação ou criar testes com um "dublê" (mock).
/// </summary>
public interface IPessoaService
{
    Task<PessoaResponseDto> CriarAsync(CreatePessoaDto dto);
    Task<IEnumerable<PessoaResponseDto>> ListarAsync();
}

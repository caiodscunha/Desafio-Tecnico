namespace DesafioTecnico.Api.DTOs;

/// <summary>
/// Resposta completa do endpoint de totais: a lista de totais por pessoa,
/// seguida do total geral somando todo mundo. Formato pedido pelo desafio:
/// "listar todas as pessoas... ao final, exibir o total geral".
/// </summary>
public class TotaisResponseDto
{
    public List<TotalPessoaDto> Pessoas { get; set; } = new();
    public TotalGeralDto TotalGeral { get; set; } = new();
}

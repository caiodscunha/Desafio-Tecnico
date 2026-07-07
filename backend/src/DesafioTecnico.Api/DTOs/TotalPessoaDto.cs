namespace DesafioTecnico.Api.DTOs;

/// <summary>
/// Totais consolidados de uma pessoa: soma de receitas, soma de despesas e o saldo
/// (receitas - despesas). Usado dentro de TotaisResponseDto, um item por pessoa cadastrada.
/// </summary>
public class TotalPessoaDto
{
    public Guid PessoaId { get; set; }
    public string Nome { get; set; } = string.Empty;
    public float TotalReceitas { get; set; }
    public float TotalDespesas { get; set; }
    public float Saldo { get; set; }
}

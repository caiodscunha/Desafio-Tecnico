namespace DesafioTecnico.Api.DTOs;

/// <summary>
/// Totais somando TODAS as pessoas juntas: total geral de receitas, de despesas
/// e o saldo líquido (receitas - despesas). É o "rodapé" da consulta de totais.
/// </summary>
public class TotalGeralDto
{
    public float TotalReceitas { get; set; }
    public float TotalDespesas { get; set; }
    public float SaldoLiquido { get; set; }
}

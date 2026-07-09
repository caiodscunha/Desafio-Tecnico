namespace DesafioTecnico.Api.DTOs;

/// <summary>
/// Totais somando TODAS as pessoas juntas: total geral de receitas, de despesas
/// e o saldo líquido (receitas - despesas). É o "rodapé" da consulta de totais.
/// </summary>
public class TotalGeralDto
{
    public decimal TotalReceitas { get; set; }
    public decimal TotalDespesas { get; set; }
    public decimal SaldoLiquido { get; set; }
}

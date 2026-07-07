using DesafioTecnico.Api.Data;
using DesafioTecnico.Api.DTOs;
using DesafioTecnico.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace DesafioTecnico.Api.Services;

/// <summary>
/// Calcula os totais de receitas, despesas e saldo por pessoa, além do total geral.
/// </summary>
public class TotaisService : ITotaisService
{
    private readonly AppDbContext _context;

    public TotaisService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<TotaisResponseDto> ObterAsync()
    {
        // Traz tudo pra memória com duas consultas simples (uma pra pessoas, outra pra
        // transações) e faz a soma em C#. Pro tamanho de dados desse desafio isso é mais
        // simples de ler do que montar um GroupBy traduzido pra SQL, e funciona igual.
        var pessoas = await _context.Pessoas.ToListAsync();
        var transacoes = await _context.Transacoes.ToListAsync();

        var totaisPorPessoa = pessoas
            .Select(pessoa =>
            {
                var transacoesDaPessoa = transacoes.Where(t => t.PessoaId == pessoa.Id);

                var totalReceitas = transacoesDaPessoa
                    .Where(t => t.Tipo == TipoTransacao.Receita)
                    .Sum(t => t.Valor);

                var totalDespesas = transacoesDaPessoa
                    .Where(t => t.Tipo == TipoTransacao.Despesa)
                    .Sum(t => t.Valor);

                return new TotalPessoaDto
                {
                    PessoaId = pessoa.Id,
                    Nome = pessoa.Nome,
                    TotalReceitas = totalReceitas,
                    TotalDespesas = totalDespesas,
                    Saldo = totalReceitas - totalDespesas
                };
            })
            .ToList();

        // Total geral = soma dos totais já calculados de cada pessoa.
        var totalGeral = new TotalGeralDto
        {
            TotalReceitas = totaisPorPessoa.Sum(p => p.TotalReceitas),
            TotalDespesas = totaisPorPessoa.Sum(p => p.TotalDespesas),
            SaldoLiquido = totaisPorPessoa.Sum(p => p.Saldo)
        };

        return new TotaisResponseDto
        {
            Pessoas = totaisPorPessoa,
            TotalGeral = totalGeral
        };
    }
}

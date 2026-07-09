using DesafioTecnico.Api.Data;
using DesafioTecnico.Api.DTOs;
using DesafioTecnico.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace DesafioTecnico.Api.Services;

/// <summary>
/// Implementação das regras de negócio de Transação: criar (com validações) e listar.
/// </summary>
public class TransacaoService : ITransacaoService
{
    private readonly AppDbContext _context;

    // O AppDbContext é injetado automaticamente pelo ASP.NET Core (configurado em Program.cs).
    public TransacaoService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<TransacaoResponseDto> CriarAsync(CreateTransacaoDto dto)
    {
        // A transação precisa pertencer a uma pessoa que realmente existe no banco.
        var pessoa = await _context.Pessoas.FindAsync(dto.PessoaId);
        if (pessoa is null)
        {
            throw new KeyNotFoundException("Pessoa não encontrada.");
        }

        // Regra de negócio: menor de 18 anos só pode cadastrar despesas, nunca receitas.
        if (pessoa.Idade < 18 && dto.Tipo == TipoTransacao.Receita)
        {
            throw new InvalidOperationException("Pessoas menores de 18 anos só podem cadastrar despesas.");
        }

        var transacao = new Transacao
        {
            Id = Guid.NewGuid(),
            Tipo = dto.Tipo,
            Descricao = dto.Descricao,
            Valor = dto.Valor,
            PessoaId = dto.PessoaId
        };

        _context.Transacoes.Add(transacao);
        await _context.SaveChangesAsync();

        return new TransacaoResponseDto
        {
            Id = transacao.Id,
            Tipo = transacao.Tipo,
            Descricao = transacao.Descricao,
            Valor = transacao.Valor,
            PessoaId = transacao.PessoaId
        };
    }

    public async Task<IEnumerable<TransacaoResponseDto>> ListarAsync()
    {
        return await _context.Transacoes
            .Select(t => new TransacaoResponseDto
            {
                Id = t.Id,
                Tipo = t.Tipo,
                Descricao = t.Descricao,
                Valor = t.Valor,
                PessoaId = t.PessoaId
            })
            .ToListAsync();
    }
}

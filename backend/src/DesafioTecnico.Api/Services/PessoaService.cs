using DesafioTecnico.Api.Data;
using DesafioTecnico.Api.DTOs;
using DesafioTecnico.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace DesafioTecnico.Api.Services;

/// <summary>
/// Implementação das regras de negócio de Pessoa.
/// Por enquanto contém apenas criar e listar (exemplo-base); exclusão em cascata e as demais
/// regras do desafio (transações, totais) serão adicionadas nas próximas etapas.
/// </summary>
public class PessoaService : IPessoaService
{
    private readonly AppDbContext _context;

    // O AppDbContext é injetado automaticamente pelo ASP.NET Core (configurado em Program.cs).
    public PessoaService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PessoaResponseDto> CriarAsync(CreatePessoaDto dto)
    {
        var pessoa = new Pessoa
        {
            Id = Guid.NewGuid(),
            Nome = dto.Nome,
            Idade = dto.Idade
        };

        _context.Pessoas.Add(pessoa);
        await _context.SaveChangesAsync();

        return new PessoaResponseDto
        {
            Id = pessoa.Id,
            Nome = pessoa.Nome,
            Idade = pessoa.Idade
        };
    }

    public async Task<IEnumerable<PessoaResponseDto>> ListarAsync()
    {
        return await _context.Pessoas
            .Select(p => new PessoaResponseDto
            {
                Id = p.Id,
                Nome = p.Nome,
                Idade = p.Idade
            })
            .ToListAsync();
    }
}

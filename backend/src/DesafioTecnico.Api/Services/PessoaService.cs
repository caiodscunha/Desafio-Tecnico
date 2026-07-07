using DesafioTecnico.Api.Data;
using DesafioTecnico.Api.DTOs;
using DesafioTecnico.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace DesafioTecnico.Api.Services;

/// <summary>
/// Implementação das regras de negócio de Pessoa: criar, listar e deletar.
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

    public async Task DeletarAsync(Guid id)
    {
        var pessoa = await _context.Pessoas.FindAsync(id);
        if (pessoa is null)
        {
            throw new KeyNotFoundException("Pessoa não encontrada.");
        }

        // A exclusão em cascata configurada em AppDbContext.OnModelCreating garante que
        // todas as transações dessa pessoa sejam apagadas automaticamente junto.
        _context.Pessoas.Remove(pessoa);
        await _context.SaveChangesAsync();
    }
}

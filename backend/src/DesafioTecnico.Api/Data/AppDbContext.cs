using DesafioTecnico.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace DesafioTecnico.Api.Data;

/// <summary>
/// Ponte entre o código C# e o banco de dados (SQLite).
/// Cada "DbSet" abaixo vira uma tabela no banco. O EF Core traduz operações em C#
/// (ex.: _context.Pessoas.Add(...)) em comandos SQL automaticamente.
/// </summary>
public class AppDbContext : DbContext
{
    // O construtor recebe as opções (ex.: connection string) via injeção de dependência,
    // configuradas em Program.cs.
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Pessoa> Pessoas => Set<Pessoa>();

    public DbSet<Transacao> Transacoes => Set<Transacao>();

    /// <summary>
    /// Configura detalhes do relacionamento entre Transacao e Pessoa que não dá pra
    /// expressar só com propriedades na classe.
    /// Aqui garantimos a regra do desafio: ao deletar uma Pessoa, todas as Transacoes
    /// dela são apagadas automaticamente pelo próprio banco (exclusão em cascata).
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Transacao>()
            .HasOne(t => t.Pessoa)          // cada Transacao tem uma Pessoa
            .WithMany()                     // uma Pessoa pode ter várias Transacoes
            .HasForeignKey(t => t.PessoaId) // PessoaId é a chave estrangeira
            .OnDelete(DeleteBehavior.Cascade); // deletar a Pessoa apaga as Transacoes dela
    }
}

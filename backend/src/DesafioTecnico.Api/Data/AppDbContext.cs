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
}

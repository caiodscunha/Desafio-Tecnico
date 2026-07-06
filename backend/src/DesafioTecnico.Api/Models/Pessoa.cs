namespace DesafioTecnico.Api.Models;

/// <summary>
/// Representa uma pessoa cadastrada no sistema de controle de gastos residenciais.
/// Esta classe é uma "entidade" — o Entity Framework Core vai usá-la para criar
/// e mapear a tabela "Pessoas" no banco de dados SQLite.
/// </summary>
public class Pessoa
{
    /// <summary>
    /// Identificador único da pessoa, gerado automaticamente na criação (ver PessoaService).
    /// Guid = "Globally Unique Identifier", um valor aleatório praticamente impossível de repetir,
    /// muito usado no lugar de números sequenciais (1, 2, 3...) como chave primária.
    /// </summary>
    public Guid Id { get; set; }

    public string Nome { get; set; } = string.Empty;

    public int Idade { get; set; }
}

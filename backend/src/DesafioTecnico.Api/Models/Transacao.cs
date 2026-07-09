namespace DesafioTecnico.Api.Models;

/// <summary>
/// Representa uma transação financeira (receita ou despesa) associada a uma pessoa.
/// Assim como Pessoa, é uma "entidade" mapeada pelo EF Core para a tabela "Transacoes".
/// </summary>
public class Transacao
{
    /// <summary>
    /// Identificador único da transação, gerado automaticamente na criação (ver TransacaoService).
    /// </summary>
    public Guid Id { get; set; }

    public TipoTransacao Tipo { get; set; }

    public string Descricao { get; set; } = string.Empty;

    public decimal Valor { get; set; }

    /// <summary>
    /// Chave estrangeira: valor real gravado no banco, apontando pra Pessoa dona da transação.
    /// </summary>
    public Guid PessoaId { get; set; }

    /// <summary>
    /// Propriedade de navegação: não vira coluna no banco. Permite acessar
    /// transacao.Pessoa.Nome sem escrever o JOIN manualmente (o EF Core faz isso
    /// usando a relação configurada em AppDbContext.OnModelCreating).
    /// </summary>
    public Pessoa Pessoa { get; set; } = null!;
}

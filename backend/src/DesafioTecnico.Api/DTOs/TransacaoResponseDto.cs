using DesafioTecnico.Api.Models;

namespace DesafioTecnico.Api.DTOs;

/// <summary>
/// Dados devolvidos ao cliente ao consultar uma transação.
/// Separado da entidade (Models/Transacao.cs) pelo mesmo motivo do PessoaResponseDto:
/// evita expor detalhes internos do banco e dá liberdade pra moldar a resposta pro front-end.
/// </summary>
public class TransacaoResponseDto
{
    public Guid Id { get; set; }
    public TipoTransacao Tipo { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public float Valor { get; set; }
    public Guid PessoaId { get; set; }
}


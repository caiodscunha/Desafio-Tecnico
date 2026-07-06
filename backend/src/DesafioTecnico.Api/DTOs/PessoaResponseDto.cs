namespace DesafioTecnico.Api.DTOs;

/// <summary>
/// Dados devolvidos ao cliente ao consultar uma pessoa.
/// Ter um DTO de saída separado da entidade (Models/Pessoa.cs) evita expor detalhes internos
/// do banco de dados e dá liberdade para moldar a resposta como o front-end precisa.
/// </summary>
public class PessoaResponseDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int Idade { get; set; }
}

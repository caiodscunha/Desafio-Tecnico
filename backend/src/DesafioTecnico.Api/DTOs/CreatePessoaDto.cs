using System.ComponentModel.DataAnnotations;

namespace DesafioTecnico.Api.DTOs;

/// <summary>
/// Dados recebidos do cliente para criar uma pessoa.
/// Repare que não tem "Id": ele é gerado pelo servidor, nunca informado pelo cliente.
/// As anotações [Required]/[Range] fazem validação automática antes do código do controller rodar.
/// </summary>
public class CreatePessoaDto
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    public string Nome { get; set; } = string.Empty;

    [Range(0, 150, ErrorMessage = "A idade deve estar entre 0 e 150.")]
    public int Idade { get; set; }
}

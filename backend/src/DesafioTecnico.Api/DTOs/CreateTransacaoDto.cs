using System.ComponentModel.DataAnnotations;
using DesafioTecnico.Api.Models;

namespace DesafioTecnico.Api.DTOs;

/// <summary>
/// Dados recebidos do cliente para criar uma transação.
/// Repare que não tem "Id": ele é gerado pelo servidor, nunca informado pelo cliente.
/// As anotações [Required]/[Range] fazem validação automática antes do código do controller rodar.
/// A regra "menor de 18 anos só pode cadastrar despesa" não dá pra validar aqui com atributos
/// simples, porque depende de outra tabela (Pessoa) — por isso ela fica no TransacaoService.
/// </summary>
public class CreateTransacaoDto
{
    [Required(ErrorMessage = "O tipo é obrigatório.")]
    public TipoTransacao Tipo { get; set; }

    [Required(ErrorMessage = "A descrição é obrigatória.")]
    public string Descricao { get; set; } = string.Empty;

    [Required(ErrorMessage = "O valor é obrigatório.")]
    [Range(0, float.MaxValue, ErrorMessage = "O valor deve ser um número positivo.")]
    public float Valor { get; set; }

    [Required(ErrorMessage = "O ID da pessoa é obrigatório.")]
    public Guid PessoaId { get; set; }
}

namespace DesafioTecnico.Api.Models;

/// <summary>
/// Tipo de uma transação financeira.
/// Um "enum" é um conjunto fixo de valores nomeados (aqui, representados como números:
/// Receita = 0, Despesa = 1). Isso evita erros de digitação que aconteceriam se usássemos texto livre.
/// </summary>
public enum TipoTransacao
{
    Receita = 0,
    Despesa = 1
}

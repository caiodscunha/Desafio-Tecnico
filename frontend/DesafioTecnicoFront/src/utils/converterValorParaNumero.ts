// Converte um valor digitado pelo usuário (que pode usar vírgula, como "150,50",
// já que o Brasil usa vírgula como separador decimal) num número de verdade.
// Number("150,50") daria NaN — por isso trocamos a vírgula por ponto antes.
export function converterValorParaNumero(valorDigitado: string): number {
  return Number(valorDigitado.replace(",", "."));
}

// As funções de api/*.ts sempre lançam um Error com a mensagem vinda do back-end
// (ver extrairMensagemDeErro em api/httpError.ts). Aqui só extraímos essa mensagem,
// com um texto genérico de reserva caso o erro não seja o esperado.
export function mensagemDoErro(erro: unknown, fallback: string): string {
  return erro instanceof Error ? erro.message : fallback;
}

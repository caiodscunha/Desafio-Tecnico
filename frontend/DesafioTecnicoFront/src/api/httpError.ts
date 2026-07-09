// Extrai uma mensagem de erro legível do corpo da resposta.
// O back-end responde erros de duas formas diferentes:
// 1) Texto puro (ex.: NotFound("Pessoa não encontrada.") no TransacoesController/PessoasController)
// 2) JSON estruturado (ex.: quando o [ApiController] rejeita um DTO inválido automaticamente,
//    tipo { "errors": { "Nome": ["O nome é obrigatório."] } })
// Essa função tenta interpretar os dois formatos e sempre devolve uma string pronta pra exibir.
export async function extrairMensagemDeErro(response: Response): Promise<string> {
  const texto = await response.text();

  try {
    const corpo = JSON.parse(texto);

    if (corpo.errors) {
      return Object.values(corpo.errors).flat().join(" ");
    }

    if (corpo.title) {
      return corpo.title;
    }
  } catch {
    // Não era JSON — o texto puro já é a mensagem de erro.
  }

  return texto || `Erro ${response.status}`;
}

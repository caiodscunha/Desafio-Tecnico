import { useEffect, useState, type SubmitEvent } from "react";
import "./TransacoesPage.css";
import type { Pessoa } from "../types/Pessoa";
import type { Transacao, TipoTransacao } from "../types/Transacao";
import { listarPessoas } from "../api/pessoasApi";
import { criarTransacao, listarTransacoes } from "../api/transacoesApi";
import { mensagemDoErro } from "../utils/mensagemDoErro";
import { converterValorParaNumero } from "../utils/converterValorParaNumero";

export function TransacoesPage() {
  const [transacoes, setTransacoes] = useState<Transacao[]>([]);
  const [pessoas, setPessoas] = useState<Pessoa[]>([]);
  const [pessoaId, setPessoaId] = useState("");
  const [tipo, setTipo] = useState<TipoTransacao>(1);
  const [descricao, setDescricao] = useState("");
  const [valor, setValor] = useState("");
  const [erro, setErro] = useState<string | null>(null);

  async function carregarDados() {
    try {
      const [transacoesCarregadas, pessoasCarregadas] = await Promise.all([
        listarTransacoes(),
        listarPessoas(),
      ]);
      setTransacoes(transacoesCarregadas);
      setPessoas(pessoasCarregadas);
    } catch (erro) {
      setErro(mensagemDoErro(erro, "Não foi possível carregar os dados."));
    }
  }

  // Array de dependências vazio ([]) = roda só uma vez, quando a tela abre.
  // Mesmo padrão do PessoasPage: a busca fica numa função async "imediatamente
  // invocada" aqui dentro, por causa da regra react-hooks/set-state-in-effect do ESLint.
  useEffect(() => {
    (async () => {
      try {
        const [transacoesCarregadas, pessoasCarregadas] = await Promise.all([
          listarTransacoes(),
          listarPessoas(),
        ]);
        setTransacoes(transacoesCarregadas);
        setPessoas(pessoasCarregadas);
      } catch (erro) {
        setErro(mensagemDoErro(erro, "Não foi possível carregar os dados."));
      }
    })();
  }, []);

  async function handleCriar(event: SubmitEvent<HTMLFormElement>) {
    event.preventDefault();
    setErro(null);

    const valorNumerico = converterValorParaNumero(valor);
    if (Number.isNaN(valorNumerico)) {
      setErro("Informe um valor válido (ex: 150,50).");
      return;
    }

    try {
      await criarTransacao({ tipo, descricao, valor: valorNumerico, pessoaId });
      setDescricao("");
      setValor("");
      await carregarDados();
    } catch (erro) {
      setErro(mensagemDoErro(erro, "Não foi possível criar a transação."));
    }
  }

  function nomeDaPessoa(id: string): string {
    return pessoas.find((pessoa) => pessoa.id === id)?.nome ?? "Pessoa removida";
  }

  return (
    <div>
      <h1>Transações</h1>

      <form className="transacoes-form" onSubmit={handleCriar}>
        <select
          value={pessoaId}
          onChange={(e) => setPessoaId(e.target.value)}
          required
        >
          <option value="">Selecione a pessoa</option>
          {pessoas.map((pessoa) => (
            <option key={pessoa.id} value={pessoa.id}>
              {pessoa.nome}
            </option>
          ))}
        </select>

        <select
          value={tipo}
          onChange={(e) => setTipo(Number(e.target.value) as TipoTransacao)}
        >
          <option value={0}>Receita</option>
          <option value={1}>Despesa</option>
        </select>

        <input
          placeholder="Descrição"
          value={descricao}
          onChange={(e) => setDescricao(e.target.value)}
          required
        />

        <input
          type="text"
          inputMode="decimal"
          placeholder="Valor (ex: 150,50)"
          value={valor}
          onChange={(e) => setValor(e.target.value)}
          required
        />

        <button type="submit">Adicionar</button>
      </form>

      {erro && <p className="transacoes-erro">{erro}</p>}

      {transacoes.length === 0 ? (
        <p className="transacoes-vazio">Nenhuma transação cadastrada ainda.</p>
      ) : (
        <ul className="transacoes-lista">
          {transacoes.map((transacao) => (
            <li key={transacao.id} className="transacoes-item">
              <span>
                <strong>{nomeDaPessoa(transacao.pessoaId)}</strong> - {transacao.descricao}
              </span>
              <span className={transacao.tipo === 0 ? "valor-receita" : "valor-despesa"}>
                {transacao.tipo === 0 ? "+" : "-"} R$ {transacao.valor.toFixed(2)}
              </span>
            </li>
          ))}
        </ul>
      )}
    </div>
  );
}

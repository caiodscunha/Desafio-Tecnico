import { useEffect, useState, type SubmitEvent } from "react";
import "./PessoasPage.css";
import type { Pessoa } from "../types/Pessoa";
import { criarPessoa, deletarPessoa, listarPessoas } from "../api/pessoasApi";
import { mensagemDoErro } from "../utils/mensagemDoErro";

export function PessoasPage() {
  const [pessoas, setPessoas] = useState<Pessoa[]>([]);
  const [nome, setNome] = useState("");
  const [idade, setIdade] = useState("");
  const [erro, setErro] = useState<string | null>(null);

  async function carregarPessoas() {
    try {
      const dados = await listarPessoas();
      setPessoas(dados);
    } catch (erro) {
      setErro(mensagemDoErro(erro, "Não foi possível carregar as pessoas."));
    }
  }

  // Array de dependências vazio ([]) = roda só uma vez, quando a tela abre.
  // O ESLint (regra react-hooks/set-state-in-effect) exige que a busca fique numa função
  // async "imediatamente invocada" aqui dentro, em vez de chamar uma função async externa
  // diretamente no corpo do efeito.
  useEffect(() => {
    (async () => {
      try {
        const dados = await listarPessoas();
        setPessoas(dados);
      } catch (erro) {
        setErro(mensagemDoErro(erro, "Não foi possível carregar as pessoas."));
      }
    })();
  }, []);

  async function handleCriar(event: SubmitEvent<HTMLFormElement>) {
    event.preventDefault();
    setErro(null);

    try {
      await criarPessoa({ nome, idade: Number(idade) });
      setNome("");
      setIdade("");
      await carregarPessoas();
    } catch (erro) {
      setErro(mensagemDoErro(erro, "Não foi possível criar a pessoa."));
    }
  }

  async function handleDeletar(id: string) {
    try {
      await deletarPessoa(id);
      await carregarPessoas();
    } catch (erro) {
      setErro(mensagemDoErro(erro, "Não foi possível excluir a pessoa."));
    }
  }

  return (
    <div>
      <h1>Pessoas</h1>

      <form className="pessoas-form" onSubmit={handleCriar}>
        <input
          placeholder="Nome"
          value={nome}
          onChange={(e) => setNome(e.target.value)}
          required
        />
        <input
          type="number"
          placeholder="Idade"
          value={idade}
          onChange={(e) => setIdade(e.target.value)}
          required
        />
        <button type="submit">Adicionar</button>
      </form>

      {erro && <p className="pessoas-erro">{erro}</p>}

      {pessoas.length === 0 ? (
        <p className="pessoas-vazio">Nenhuma pessoa cadastrada ainda.</p>
      ) : (
        <ul className="pessoas-lista">
          {pessoas.map((pessoa) => (
            <li key={pessoa.id} className="pessoas-item">
              <span>
                {pessoa.nome} ({pessoa.idade} anos)
              </span>
              <button className="secundario" onClick={() => handleDeletar(pessoa.id)}>
                Excluir
              </button>
            </li>
          ))}
        </ul>
      )}
    </div>
  );
}

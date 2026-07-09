import { useEffect, useState } from "react";
import "./TotaisPage.css";
import type { Totais } from "../types/Totais";
import { obterTotais } from "../api/totaisApi";
import { mensagemDoErro } from "../utils/mensagemDoErro";
import { formatarMoeda } from "../utils/formatarMoeda";

export function TotaisPage() {
  const [totais, setTotais] = useState<Totais | null>(null);
  const [erro, setErro] = useState<string | null>(null);

  // Mesmo padrão de busca das outras páginas: função async "imediatamente invocada"
  // dentro do efeito, por causa da regra react-hooks/set-state-in-effect do ESLint.
  useEffect(() => {
    (async () => {
      try {
        const dados = await obterTotais();
        setTotais(dados);
      } catch (erro) {
        setErro(mensagemDoErro(erro, "Não foi possível carregar os totais."));
      }
    })();
  }, []);

  function classeSaldo(valor: number): string {
    return valor >= 0 ? "totais-saldo-positivo" : "totais-saldo-negativo";
  }

  return (
    <div>
      <h1>Totais</h1>

      {erro && <p className="totais-erro">{erro}</p>}

      {!erro && !totais && <p className="totais-vazio">Carregando...</p>}

      {totais && totais.pessoas.length === 0 && (
        <p className="totais-vazio">Nenhuma pessoa cadastrada ainda.</p>
      )}

      {totais && totais.pessoas.length > 0 && (
        <table className="totais-tabela">
          <thead>
            <tr>
              <th>Pessoa</th>
              <th>Receitas</th>
              <th>Despesas</th>
              <th>Saldo</th>
            </tr>
          </thead>
          <tbody>
            {totais.pessoas.map((pessoa) => (
              <tr key={pessoa.pessoaId}>
                <td>{pessoa.nome}</td>
                <td>{formatarMoeda(pessoa.totalReceitas)}</td>
                <td>{formatarMoeda(pessoa.totalDespesas)}</td>
                <td className={classeSaldo(pessoa.saldo)}>
                  {formatarMoeda(pessoa.saldo)}
                </td>
              </tr>
            ))}
          </tbody>
          <tfoot>
            <tr>
              <td>Total geral</td>
              <td>{formatarMoeda(totais.totalGeral.totalReceitas)}</td>
              <td>{formatarMoeda(totais.totalGeral.totalDespesas)}</td>
              <td className={classeSaldo(totais.totalGeral.saldoLiquido)}>
                {formatarMoeda(totais.totalGeral.saldoLiquido)}
              </td>
            </tr>
          </tfoot>
        </table>
      )}
    </div>
  );
}

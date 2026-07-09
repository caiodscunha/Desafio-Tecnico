import "./Header.css";
import type { Pagina } from "../types/Pagina";

interface HeaderProps {
  paginaAtiva: Pagina;
  aoTrocarPagina: (pagina: Pagina) => void;
}

const OPCOES: { pagina: Pagina; rotulo: string }[] = [
  { pagina: "pessoas", rotulo: "Pessoas" },
  { pagina: "transacoes", rotulo: "Transações" },
  { pagina: "totais", rotulo: "Totais" },
];

export function Header({ paginaAtiva, aoTrocarPagina }: HeaderProps) {
  return (
    <header className="header">
      <h1 className="header-titulo">Controle de Gastos Residenciais</h1>

      <nav className="header-nav">
        {OPCOES.map((opcao) => (
          <button
            key={opcao.pagina}
            className={opcao.pagina === paginaAtiva ? "nav-ativo" : "nav-inativo"}
            onClick={() => aoTrocarPagina(opcao.pagina)}
          >
            {opcao.rotulo}
          </button>
        ))}
      </nav>
    </header>
  );
}

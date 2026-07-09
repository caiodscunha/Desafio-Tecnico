import { useState } from "react";
import "./App.css";
import { Header } from "./components/Header";
import { PessoasPage } from "./pages/PessoasPage";
import { TransacoesPage } from "./pages/TransacoesPage";
import { TotaisPage } from "./pages/TotaisPage";
import type { Pagina } from "./types/Pagina";

function App() {
  const [paginaAtiva, setPaginaAtiva] = useState<Pagina>("pessoas");

  return (
    <div className="app-container">
      <Header paginaAtiva={paginaAtiva} aoTrocarPagina={setPaginaAtiva} />

      {paginaAtiva === "pessoas" && <PessoasPage />}
      {paginaAtiva === "transacoes" && <TransacoesPage />}
      {paginaAtiva === "totais" && <TotaisPage />}
    </div>
  );
}

export default App;

// Formato de cada item retornado por GET /api/totais (ver TotalPessoaDto.cs no backend).
export interface TotalPessoa {
  pessoaId: string;
  nome: string;
  totalReceitas: number;
  totalDespesas: number;
  saldo: number;
}

// Soma de todas as pessoas juntas (ver TotalGeralDto.cs no backend).
export interface TotalGeral {
  totalReceitas: number;
  totalDespesas: number;
  saldoLiquido: number;
}

// Resposta completa do endpoint (ver TotaisResponseDto.cs no backend).
export interface Totais {
  pessoas: TotalPessoa[];
  totalGeral: TotalGeral;
}

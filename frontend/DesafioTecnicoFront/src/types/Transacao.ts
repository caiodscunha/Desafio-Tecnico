export type TipoTransacao = 0 | 1; // 0 = Receita, 1 = Despesa

// Formato de uma transação como o back-end devolve (ver TransacaoResponseDto.cs no backend).
export interface Transacao {
  id: string;
  tipo: TipoTransacao;
  descricao: string;
  valor: number;
  pessoaId: string;
}

// Dados que o front precisa enviar pra criar uma transação (sem "id": quem gera é o back-end).
export interface CreateTransacaoInput {
  tipo: TipoTransacao;
  descricao: string;
  valor: number;
  pessoaId: string;
}
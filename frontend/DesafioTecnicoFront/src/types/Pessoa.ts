// Formato de uma pessoa como o back-end devolve (ver PessoaResponseDto.cs no backend).
export interface Pessoa {
  id: string;
  nome: string;
  idade: number;
}

// Dados que o front precisa enviar pra criar uma pessoa (sem "id": quem gera é o back-end).
export interface CreatePessoaInput {
  nome: string;
  idade: number;
}

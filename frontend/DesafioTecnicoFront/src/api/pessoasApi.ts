import { API_BASE_URL } from "./config";
import { extrairMensagemDeErro } from "./httpError";
import type { CreatePessoaInput, Pessoa } from "../types/Pessoa";

export async function listarPessoas(): Promise<Pessoa[]> {
  const response = await fetch(`${API_BASE_URL}/pessoas`);

  if (!response.ok) {
    throw new Error(await extrairMensagemDeErro(response));
  }

  return response.json();
}

export async function criarPessoa(dados: CreatePessoaInput): Promise<Pessoa> {
  const response = await fetch(`${API_BASE_URL}/pessoas`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(dados),
  });

  if (!response.ok) {
    throw new Error(await extrairMensagemDeErro(response));
  }

  return response.json();
}

export async function deletarPessoa(id: string): Promise<void> {
  const response = await fetch(`${API_BASE_URL}/pessoas/${id}`, {
    method: "DELETE",
  });

  if (!response.ok) {
    throw new Error(await extrairMensagemDeErro(response));
  }
}

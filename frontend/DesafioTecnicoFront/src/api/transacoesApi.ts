import { API_BASE_URL } from "./config";
import { extrairMensagemDeErro } from "./httpError";
import type { CreateTransacaoInput, Transacao } from "../types/Transacao";

export async function listarTransacoes(): Promise<Transacao[]> {
  const response = await fetch(`${API_BASE_URL}/transacoes`);

  if (!response.ok) {
    throw new Error(await extrairMensagemDeErro(response));
  }

  return response.json();
}

export async function criarTransacao(dados: CreateTransacaoInput): Promise<Transacao> {
  const response = await fetch(`${API_BASE_URL}/transacoes`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(dados),
  });

  if (!response.ok) {
    throw new Error(await extrairMensagemDeErro(response));
  }

  return response.json();
}

import { API_BASE_URL } from "./config";
import { extrairMensagemDeErro } from "./httpError";
import type { Totais } from "../types/Totais";

export async function obterTotais(): Promise<Totais> {
  const response = await fetch(`${API_BASE_URL}/totais`);

  if (!response.ok) {
    throw new Error(await extrairMensagemDeErro(response));
  }

  return response.json();
}

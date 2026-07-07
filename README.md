<div align="center">

<img src="https://img.shields.io/badge/version-1.0.0-blue?style=for-the-badge" alt="Version" />
<img src="https://img.shields.io/badge/backend-conclu%C3%ADdo-brightgreen?style=for-the-badge" alt="Backend Status" />
<img src="https://img.shields.io/badge/frontend-em%20desenvolvimento-yellow?style=for-the-badge" alt="Frontend Status" />
<img src="https://img.shields.io/badge/.NET-10-512BD4?style=for-the-badge&logo=dotnet" alt=".NET" />

<br/><br/>

# Sistema de Controle de Gastos Residenciais

### *Cadastre pessoas, registre receitas e despesas, e acompanhe o saldo de cada uma.*

Desafio técnico de back-end/front-end: uma API que permite cadastrar pessoas de uma residência, registrar as transações financeiras (receitas e despesas) de cada uma, e consultar os totais consolidados, individuais e gerais.

<br/>

[💡 Sobre](#-sobre-o-projeto) · [✨ Funcionalidades](#-funcionalidades) · [📐 Regras de Negócio](#-regras-de-negócio) · [🛠 Stack](#-stack-tecnológica) · [▶️ Como Rodar](#️-como-rodar-o-back-end) · [🔌 Endpoints](#-endpoints-da-api)

</div>

---

## 📋 Índice

- [Sobre o Projeto](#-sobre-o-projeto)
- [Funcionalidades](#-funcionalidades)
- [Regras de Negócio](#-regras-de-negócio)
- [Stack Tecnológica](#-stack-tecnológica)
- [Estrutura de Pastas](#-estrutura-de-pastas-back-end)
- [Como Rodar o Back-end](#️-como-rodar-o-back-end)
- [Endpoints da API](#-endpoints-da-api)

---

## 💡 Sobre o Projeto

Este projeto resolve um problema simples e comum: **organizar os gastos de uma casa entre as pessoas que moram nela**. Cada pessoa cadastrada pode ter suas próprias transações (receitas e despesas), e o sistema calcula automaticamente o saldo de cada uma, além do saldo geral da residência.

O back-end é uma API REST em **.NET / C#**, com persistência em **SQLite** (os dados continuam salvos mesmo depois de fechar a aplicação). O front-end, em **React + TypeScript**, consome essa API para exibir os cadastros e os totais.

---

## ✨ Funcionalidades

### 👤 Cadastro de Pessoas
- Criação, listagem e exclusão
- Ao excluir uma pessoa, todas as suas transações são apagadas automaticamente (exclusão em cascata)
- Campos: `Id` (Guid, gerado automaticamente), `Nome`, `Idade`

### 💸 Cadastro de Transações
- Criação e listagem
- Campos: `Id` (Guid, gerado automaticamente), `Descricao`, `Valor`, `Tipo` (Receita/Despesa), `PessoaId`

### 📊 Consulta de Totais
- Lista todas as pessoas cadastradas com o total de receitas, total de despesas e o saldo (receita − despesa) de cada uma
- Ao final, exibe o total geral: soma de receitas, soma de despesas e saldo líquido de todas as pessoas juntas

---

## 📐 Regras de Negócio

| Regra | Onde é aplicada |
|---|---|
| Identificadores de Pessoa e Transação são gerados automaticamente (Guid) | `PessoaService` / `TransacaoService` |
| Toda transação precisa pertencer a uma pessoa que exista no cadastro | `TransacaoService.CriarAsync` (retorna `404` se a pessoa não existir) |
| Pessoa menor de 18 anos só pode cadastrar transações do tipo **Despesa** | `TransacaoService.CriarAsync` (retorna `400` se violado) |
| Ao excluir uma pessoa, todas as suas transações são excluídas junto | `AppDbContext.OnModelCreating` (exclusão em cascata no banco) |
| Saldo de uma pessoa = total de receitas − total de despesas | `TotaisService.ObterAsync` |
| Saldo líquido geral = soma dos saldos de todas as pessoas | `TotaisService.ObterAsync` |

---

## 🛠 Stack Tecnológica

| Camada | Tecnologia |
|---|---|
| **Back-end** | .NET 10 · C# · ASP.NET Core Web API (Controllers) |
| **ORM** | Entity Framework Core |
| **Banco de Dados** | SQLite (arquivo local `gastos.db`, persistente) |
| **Documentação da API** | Swagger / Swashbuckle |
| **Front-end** | React + TypeScript *(em desenvolvimento)* |

---

## 📂 Estrutura de Pastas (Back-end)

```
backend/
└── DesafioTecnico.sln
    └── src/
        └── DesafioTecnico.Api/
            ├── Program.cs                     # entry point (main): DI, EF Core, Swagger, CORS, pipeline HTTP
            ├── appsettings.json                # connection string do SQLite
            ├── Controllers/
            │   ├── PessoasController.cs        # POST, GET, DELETE /api/pessoas
            │   ├── TransacoesController.cs     # POST, GET /api/transacoes
            │   └── TotaisController.cs         # GET /api/totais
            ├── Models/
            │   ├── Pessoa.cs                   # entidade: Id, Nome, Idade
            │   ├── Transacao.cs                # entidade: Id, Descricao, Valor, Tipo, PessoaId, Pessoa
            │   └── TipoTransacao.cs            # enum: Receita = 0, Despesa = 1
            ├── DTOs/
            │   ├── CreatePessoaDto.cs / PessoaResponseDto.cs
            │   ├── CreateTransacaoDto.cs / TransacaoResponseDto.cs
            │   └── TotalPessoaDto.cs / TotalGeralDto.cs / TotaisResponseDto.cs
            ├── Data/
            │   └── AppDbContext.cs             # mapeamento pro banco + exclusão em cascata
            └── Services/
                ├── IPessoaService.cs / PessoaService.cs
                ├── ITransacaoService.cs / TransacaoService.cs
                └── ITotaisService.cs / TotaisService.cs
```

---

## ▶️ Como Rodar o Back-end

### Pré-requisitos
- [.NET SDK 10](https://dotnet.microsoft.com/download) (ou compatível)

### Passo a passo

```bash
cd backend/src/DesafioTecnico.Api
dotnet run
```

Na primeira execução, o banco SQLite (`gastos.db`) é criado automaticamente na pasta do projeto, já com as tabelas necessárias. Não é preciso configurar nada.

O terminal vai mostrar a porta em que a API subiu, por exemplo:
```
Now listening on: http://localhost:5053
```

Com o servidor rodando, abra `http://localhost:5053/swagger` (troque a porta pela que aparecer no seu terminal) para testar todos os endpoints interativamente pelo navegador.

> **Nota para desenvolvimento:** o banco é criado via `EnsureCreated()` (sem migrations, por simplicidade). Se você alterar alguma classe em `Models/`, apague o arquivo `gastos.db` antes de rodar de novo, para que o banco seja recriado com o schema atualizado.

---

## 🔌 Endpoints da API

### Pessoas

| Método | Rota | Descrição |
|---|---|---|
| `POST` | `/api/pessoas` | Cria uma nova pessoa |
| `GET` | `/api/pessoas` | Lista todas as pessoas |
| `DELETE` | `/api/pessoas/{id}` | Remove uma pessoa e todas as suas transações |

<details>
<summary>Exemplo: <code>POST /api/pessoas</code></summary>

```json
// Requisição
{
  "nome": "Maria",
  "idade": 30
}

// Resposta (201 Created)
{
  "id": "d4ff29e4-54ea-4ed4-a22a-bc2343096856",
  "nome": "Maria",
  "idade": 30
}
```
</details>

### Transações

| Método | Rota | Descrição |
|---|---|---|
| `POST` | `/api/transacoes` | Cria uma nova transação (`0` = Receita, `1` = Despesa) |
| `GET` | `/api/transacoes` | Lista todas as transações |

<details>
<summary>Exemplo: <code>POST /api/transacoes</code></summary>

```json
// Requisição
{
  "tipo": 1,
  "descricao": "Supermercado",
  "valor": 150.50,
  "pessoaId": "d4ff29e4-54ea-4ed4-a22a-bc2343096856"
}

// Resposta (201 Created)
{
  "id": "fc2b833c-1bf2-42a0-9f9e-9e587ef8c8be",
  "tipo": 1,
  "descricao": "Supermercado",
  "valor": 150.5,
  "pessoaId": "d4ff29e4-54ea-4ed4-a22a-bc2343096856"
}
```

Se a pessoa informada for menor de 18 anos e o tipo for `0` (Receita), a API retorna `400 Bad Request`.
</details>

### Totais

| Método | Rota | Descrição |
|---|---|---|
| `GET` | `/api/totais` | Lista o total de receitas, despesas e saldo de cada pessoa, seguido do total geral |

<details>
<summary>Exemplo: <code>GET /api/totais</code></summary>

```json
{
  "pessoas": [
    {
      "pessoaId": "d4ff29e4-54ea-4ed4-a22a-bc2343096856",
      "nome": "Maria",
      "totalReceitas": 5000,
      "totalDespesas": 1700,
      "saldo": 3300
    }
  ],
  "totalGeral": {
    "totalReceitas": 5000,
    "totalDespesas": 1700,
    "saldoLiquido": 3300
  }
}
```
</details>

---

<div align="center">

<a href="#-sistema-de-controle-de-gastos-residenciais">Voltar ao topo ↑</a>

</div>

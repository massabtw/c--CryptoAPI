# Crypto API Tracker 🪙

Este projeto coleta automaticamente os preços de 5 criptomoedas pré-definidas utilizando a API pública da CoinGecko. Os dados são armazenados localmente em formato JSON, permitindo que o usuário visualize o preço atual de cada moeda e consulte o histórico de cotações.

## 🪙 Moedas Monitoradas
O projeto atualmente rastreia as seguintes criptomoedas:
- Bitcoin
- Ethereum
- Solana
- Cardano
- Ripple

## 🔄 De Python para C# (O Propósito do Projeto)

Como desenvolvedor backend com background em Python, decidi reconstruir este projeto — que eu já havia criado anteriormente — como meu primeiro contato prático e oficial com C#. 

O objetivo principal aqui foi utilizar um ecossistema e uma lógica de negócio que eu já dominava para focar 100% na consolidação da sintaxe, dos fundamentos estruturais e da arquitetura do .NET a partir do zero.

## 📍 Endpoints da API

A aplicação disponibiliza as seguintes rotas para você interagir com os dados:

* **`GET /cryptos`**
  Retorna o último status atualizado de TODAS as criptomoedas que estão no banco de dados.

* **`GET /crypto/{moeda}`**
  Mostra apenas os dados da moeda da sua escolha. (Exemplo de uso: `/crypto/bitcoin` ou `/crypto/solana`).

* **`GET /cryptos/atualizar`**
  É o gatilho principal. Ele vai até a API da CoinGecko, atualiza o banco de dados local com os preços mais recentes e permite que você visualize esses dados novos repetindo o processo nas rotas acima.

## 🚀 Como executar o projeto

1. Clone este repositório na sua máquina.
2. Abra a Solução (`.sln`) no Visual Studio.
3. No terminal, caso precise restaurar as dependências, rode:
   ```bash
   dotnet restore
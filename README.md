# 🎵 PopVault (AI-Powered Music Manager)

> **Status:** 🚧 Em desenvolvimento (Building in Public)

O **PopVault** é uma API moderna para gerenciamento de coleções musicais, desenvolvida com o objetivo de unir as melhores práticas de Engenharia de Software (.NET 8, Clean Architecture) com o poder da Inteligência Artificial Generativa.

O diferencial deste projeto não é apenas o código, mas a **metodologia de desenvolvimento**: uma colaboração híbrida onde a IA cuida da infraestrutura repetitiva e a desenvolvedora foca na lógica de negócios complexa e integração de IA.

## 🎯 O Objetivo
Criar um "Crítico Musical Virtual". O sistema permite cadastrar álbuns e artistas, e utiliza um agente de IA para gerar automaticamente:
- Resenhas críticas do álbum.
- Análise de impacto cultural.
- Sugestões de gêneros baseadas na sonoridade.

## 🛠️ Tecnologias e Arquitetura

O projeto segue estritamente a **Clean Architecture** para garantir desacoplamento e testabilidade.

* **Linguagem:** C# (.NET 8)
* **Arquitetura:** Clean Architecture (Domain, Application, Infrastructure, API)
* **Banco de Dados:** Entity Framework Core (SQLite/SQL Server)
* **AI Integration:** Semantic Kernel (Conectando com LLMs para geração de texto)
* **Ambiente:** Desenvolvido em Linux (Pop!_OS)

## 📂 Estrutura do Projeto

```text
PopVault/
├── src/
│   ├── PopVault.Domain/           # O Coração (Entidades e Interfaces)
│   ├── PopVault.Application/      # Casos de Uso e DTOs
│   ├── PopVault.Infrastructure/   # Banco de Dados e Serviços de IA
│   └── PopVault.API/              # Endpoints RESTful
# 🧾 Documentação do Sistema de Vendas e Estoque

Bem-vindo à documentação oficial da API e do sistema de controle de vendas, produtos, estoque e caixa.  
Aqui você encontrará todas as informações necessárias para entender, manter e evoluir o sistema.

---

## 📚 Sumário

### 📌 Visão Geral
- [Regras de Negócio](./regras-negocio.md)
- [Casos de Uso](./casos-de-uso.md)
- [Modelo de Dados (Entidades)](./entidades.md)

### 🔄 Fluxos de Processo
- [Fluxo de Caixa](./fluxos/fluxo-caixa.md)
- [Fluxo de Estoque](./fluxos/fluxo-estoque.md)
- [Fluxo de Venda](./fluxos/fluxo-venda.md)

### 📡 Endpoints da API
- [Produtos](./endpoints/produtos.md)
- [Vendas](./endpoints/vendas.md)
- [Clientes](./endpoints/clientes.md)
- [Usuários e Autenticação](./endpoints/auth.md)
- [Relatórios](./endpoints/relatorios.md)

### 🧩 Arquitetura e Integrações
- [Camadas da API](./arquitetura.md)
- [Integração com o WPF](./wpf-integracao.md)
- [Planejamento de autenticação JWT](./jwt-plano.md)

---

## 🛠️ Como contribuir

Para atualizar a documentação:
1. Edite os arquivos `.md` localmente.
2. Commit e push no branch principal.
3. A Wiki será atualizada automaticamente (se estiver vinculada à pasta `/docs`).

---

## 🗂️ Estrutura Técnica

- Backend: ASP.NET Core Web API + EF Core
- Frontend: WPF (.NET 6+)
- Banco de Dados: SQL Server
- Autenticação planejada: JWT
- Versionamento: Git (Azure DevOps)

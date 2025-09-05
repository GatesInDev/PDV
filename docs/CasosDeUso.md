# 📄 Casos de Uso – Sistema de Vendas e Estoque

## UC01 - Login / Logout
**Ator:** Usuário do Sistema  
**Descrição:** Permite que qualquer usuário autenticado acesse ou saia do sistema.  
**Fluxo principal:**
1. Usuário informa usuário e senha.
2. Sistema valida as credenciais.
3. Acesso liberado conforme o perfil do usuário.
4. O usuário pode realizar logout a qualquer momento.

---

## UC02 - Abrir Caixa
**Ator:** Operador  
**Pré-condição:** Caixa fechado e usuário autenticado.  
**Descrição:** Inicia o caixa do dia para permitir vendas.  
**Fluxo principal:**
1. Operador acessa a opção "Abrir Caixa".
2. Informa valor inicial (se necessário).
3. Sistema registra data, hora e operador responsável.

---

## UC03 - Registrar Venda
**Ator:** Operador  
**Pré-condições:** Caixa aberto.  
**Regras de Negócio:** RN005, RN006, RN009  
**Descrição:** Realiza a venda de produtos.  
**Fluxo principal:**
1. Operador seleciona produtos e quantidades.
2. Sistema calcula o total e aplica descontos se necessário.
3. Operador informa forma de pagamento.
4. Sistema registra a venda, reduz o estoque e registra no caixa.

---

## UC04 - Fechar Caixa
**Ator:** Operador  
**Descrição:** Encerra o caixa ao final do dia ou turno.  
**Regras de Negócio:** RN008  
**Fluxo principal:**
1. Operador acessa "Fechar Caixa".
2. Sistema apresenta resumo das vendas e saldo.
3. Operador confirma valores e encerra o caixa.

---

## UC05 - Cadastrar Produto
**Ator:** Estoquista, Administrador  
**Descrição:** Registra novos produtos no sistema.  
**Regras de Negócio:** RN001, RN002  
**Fluxo principal:**
1. Usuário preenche nome, preço, SKU e categoria.
2. Sistema valida os dados.
3. Produto é cadastrado no estoque.

---

## UC06 - Registrar Movimentação de Estoque
**Ator:** Estoquista, Administrador  
**Descrição:** Permite entrada, saída ou ajuste de estoque.  
**Regras de Negócio:** RN004  
**Fluxo principal:**
1. Usuário seleciona tipo de movimentação.
2. Informa produto, quantidade e motivo.
3. Sistema atualiza o estoque.

---

## UC07 - Consultar Relatório de Venda
**Atores:** Gerente, Administrador  
**Descrição:** Visualiza relatório com histórico de vendas.  
**Regras de Negócio:** RN014, RN015  
**Fluxo principal:**
1. Usuário acessa seção de relatórios.
2. Define período e filtros.
3. Sistema exibe dados em tempo real.

---

## UC08 - Consultar Relatório de Estoque
**Atores:** Gerente, Administrador  
**Descrição:** Visualiza a situação atual do estoque.  
**Regras de Negócio:** RN014, RN015  
**Fluxo principal:**
1. Usuário acessa seção de relatórios.
2. Sistema mostra lista de produtos com quantidades.

---

## UC09 - Gerenciar Usuários
**Ator:** Administrador  
**Descrição:** Permite o cadastro, edição, exclusão e listagem de usuários.  
**Regras de Negócio:** RN012, RN013  
**Fluxo principal:**
1. Administrador acessa a seção de usuários.
2. Pode criar novos usuários com perfis (Operador, Estoquista, Gerente).
3. Pode editar ou excluir usuários existentes.

---

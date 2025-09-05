# Regras de Negócio

Este documento define as regras de negócio que devem ser respeitadas pela API do sistema de vendas e estoque.

---

## Produtos & Estoque

### RN001 - Produto com dados obrigatórios
- Nome, Preço, SKU, e Categoria são campos obrigatórios.
- Preço deve ser maior que zero.

### RN002 - SKU único
- Não é permitido cadastrar dois produtos com o mesmo SKU.

### RN003 - Produto inativo
- Produtos inativos não aparecem nas listagens e não podem ser vendidos.

### RN004 - Movimentação de estoque
- Não é possível movimentar estoque negativo.
- Tipos de movimentação: entrada, saída, ajuste.

### RN005 - Venda reduz estoque
- Ao realizar uma venda, o estoque dos produtos vendidos deve ser reduzido automaticamente.

---

## Vendas & Caixa

### RN006 - Venda com caixa fechado
- Não é permitido registrar vendas se o caixa estiver fechado.

### RN007 - Abertura de caixa obrigatória
- O usuário deve abrir o caixa antes da primeira venda do dia.

### RN008 - Fechamento com saldo
- Ao fechar o caixa, o sistema deve calcular o saldo final e diferenças.

### RN009 - Registro de pagamento
- Toda venda deve registrar forma de pagamento, valores, troco e usuário do caixa.

---

## Clientes & Fidelidade

### RN010 - CPF único
- Cada cliente deve ter um CPF único no sistema.

### RN011 - Acúmulo de pontos (opcional)
- Clientes acumulam pontos somente em vendas pagas.

---

## Usuários & Acesso

### RN012 - Acesso por perfil
- O sistema possui três perfis de acesso:
  - Operador: pode vender e abrir/fechar caixa.
  - Gerente: pode ver vendas e estoque.
  - Administrador: acesso completo.

### RN013 - Listagem restrita de usuários
- Apenas administradores podem visualizar ou gerenciar a lista de usuários.

---

## Relatórios & Dashboards

### RN014 - Relatórios visíveis conforme perfil
- Somente gerentes e administradores podem acessar os relatórios.

### RN015 - Dados em tempo real
- Relatórios devem refletir os dados atualizados do banco sem delay artificial.


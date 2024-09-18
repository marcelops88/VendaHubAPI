# VendaHubAPI

**VendaHubAPI** é uma API responsável por todas as operações relacionadas a vendas na empresa 123Vendas. Esta API fornece funcionalidades para criar, atualizar, recuperar e cancelar vendas, bem como gerenciar itens e aplicar descontos.

## Funcionalidades

- **Gerenciamento de Vendas**:
  - Criar nova venda
  - Atualizar venda existente
  - Recuperar detalhes da venda
  - Cancelar venda

- **Gerenciamento de Itens**:
  - Adicionar itens à venda
  - Atualizar itens da venda
  - Remover itens da venda

- **Aplicação de Descontos**:
  - Aplicar desconto percentual
  - Aplicar desconto por valor fixo

## Tecnologias Utilizadas

- .NET 6
- ASP.NET Core
- Entity Framework Core
- Serilog para logging
- FluentValidation para validação
- XUnit, FluentAssertions, Bogus e NSubstitute para testes
- TestContainer para testes de integração

## Estrutura do Projeto

- **API**: Contém controladores e configurações relacionadas à exposição da API.
- **Domain**: Contém as entidades, agregados e regras de domínio.
- **Data**: Contém o contexto do banco de dados e repositórios.

## Configuração

1. **Clone o Repositório**

   ```bash
   git clone https://github.com/seu-usuario/vendahubapi.git
   cd vendahubapi

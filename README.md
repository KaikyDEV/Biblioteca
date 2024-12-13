# Sistema de Gerenciamento de Biblioteca

Este é um sistema de gerenciamento de biblioteca simples desenvolvido em C# com conceitos avançados, como **Injeção de Dependência**, **Assincronicidade (async/await)**, **Expressões Lambda**, **LINQ**, **Logging**, e **Validação de Entrada**. O sistema permite gerenciar livros, incluindo funcionalidades para adicionar, atualizar, excluir e buscar livros por autor ou gênero.

## Funcionalidades

- **Listar livros**: Exibe todos os livros cadastrados.
- **Adicionar livro**: Permite adicionar novos livros à biblioteca.
- **Atualizar livro**: Permite atualizar os dados de um livro existente.
- **Excluir livro**: Exclui um livro baseado no seu ID.
- **Buscar livros por autor**: Pesquisa livros de um autor específico.
- **Buscar livros por gênero**: Pesquisa livros de um gênero específico.

## Tecnologias Utilizadas

- **C#** (versão mais recente do .NET Core ou .NET 6/7)
- **Injeção de Dependência (Dependency Injection)**
- **LINQ e Expressões Lambda**
- **Assíncrono (async/await)**
- **Microsoft.Extensions.Logging** para logs
- **Microsoft.Extensions.DependencyInjection** para DI

## Requisitos

Para rodar este projeto, você precisará das seguintes ferramentas instaladas:

- **.NET SDK**: [Baixar .NET SDK](https://dotnet.microsoft.com/download/dotnet)
- **Editor de código**: Como o [Visual Studio](https://visualstudio.microsoft.com/) ou [Visual Studio Code](https://code.visualstudio.com/)

## Como Rodar

### 1. Clone o repositório

```bash
git clone https://github.com/SEU-USUARIO/nome-do-repositorio.git
2. Navegue até o diretório do projeto
bash
Copiar código
cd nome-do-repositorio
3. Restaure os pacotes NuGet
bash
Copiar código
dotnet restore
4. Compile o projeto
bash
Copiar código
dotnet build
5. Execute o projeto
bash
Copiar código
dotnet run
6. Interaja com o sistema
Após rodar o sistema, o menu será exibido no terminal, permitindo a interação com o sistema de biblioteca.

Estrutura do Projeto
O projeto segue uma arquitetura modular com os seguintes componentes principais:

Modelos (Livro): Representam os dados de um livro.
Repositório (LivroRepository): Simula o armazenamento de dados, onde operações como adicionar, atualizar, excluir e buscar livros são realizadas.
Serviço (BibliotecaService): Contém a lógica de negócios para manipulação dos livros.
Interface de Usuário (BibliotecaUI): Fornece a interação do usuário com o sistema através do console.
Ponto de entrada (Program): Configura a injeção de dependência e inicializa o aplicativo.
Como Funciona
1. Injeção de Dependência
O sistema utiliza a biblioteca Microsoft.Extensions.DependencyInjection para configurar e injetar as dependências necessárias. Isso torna o código mais modular, testável e fácil de manter.

2. Assíncrono (async/await)
Todas as operações que envolvem manipulação de dados, como adição, atualização, exclusão e busca de livros, são realizadas de maneira assíncrona, utilizando as palavras-chave async e await.

3. Logging
O sistema utiliza a biblioteca Microsoft.Extensions.Logging para registrar informações importantes sobre as operações realizadas, como adição e exclusão de livros, permitindo a rastreabilidade e auditoria do sistema.

4. Validação de Entrada
Antes de realizar operações de adicionar ou atualizar livros, o sistema valida as entradas do usuário, garantindo que os dados sejam consistentes e válidos.

Contribuindo
Se você deseja contribuir para este projeto, siga os seguintes passos:

Faça um fork deste repositório.
Crie uma branch com a sua feature (git checkout -b minha-feature).
Faça as alterações necessárias e commit suas mudanças (git commit -am 'Adiciona nova funcionalidade').
Push para a branch (git push origin minha-feature).
Abra um Pull Request explicando as modificações.
Licença
Este projeto está licenciado sob a MIT License - veja o arquivo LICENSE para mais detalhes.

Autor
Kaiky Santos da Silva

### Explicação das Seções:

- **Descrição do Projeto**: Explica brevemente o que o sistema faz e as funcionalidades principais.
- **Tecnologias Utilizadas**: Apresenta as ferramentas e tecnologias principais usadas no desenvolvimento do sistema.
- **Requisitos**: Lista as ferramentas necessárias para rodar o projeto.
- **Como Rodar**: Passo a passo para configurar e rodar o projeto na máquina local.
- **Estrutura do Projeto**: Descrição da estrutura do código, detalhando os principais componentes do sistema.
- **Como Funciona**: Explica brevemente a lógica interna do sistema, como injeção de dependência, assincronicidade, logging, etc.
- **Contribuindo**: Guia para outras pessoas contribuírem para o projeto.
- **Licença**: Informações sobre a licença do projeto (no caso, MIT, mas você pode mudar conforme necessário).
- **Autor**: Sua informação para que outros possam saber quem criou o projeto e como contatá-lo.

Esse modelo pode ser adaptado conforme necessário, dependendo de modificações que você venha a fazer no código ou outros aspectos do projeto.



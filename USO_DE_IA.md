# Uso de Inteligência Artificial

Registro do uso de IA no desenvolvimento do **Catálogo de Cursos SESI**, conforme o item 4.d do enunciado do desafio.

## 1. Ferramenta utilizada

- **Claude**, da Anthropic, pela interface de chat (claude.ai), em uma conversa contínua ao longo do desenvolvimento.
- Recursos usados: leitura de imagens (capturas do Figma, de erros e de respostas JSON), leitura de arquivos enviados (PDF do enunciado e arquivos compactados do repositório), pesquisa na web e execução de código em um ambiente isolado da própria ferramenta (compilar o front-end e testar o servidor com uma API simulada).
- Versão do modelo: [CONFIRMAR: informar a versão exibida na interface do Claude].

## 2. Objetivo do uso

| Etapa | Para que a IA foi usada |
|---|---|
| Front-end (layout) | Converter capturas do protótipo do Figma em HTML e CSS: números de impacto, certificações, topo da página inicial, depoimentos, chamada com vídeo, página de detalhes do curso e seções que abrem e fecham |
| Front-end (lógica) | Integrar o Angular à API (cliente HTTP, serviço, modelos), carrossel com rolagem, rotas, página de detalhes, estados de carregamento e erro |
| Back-end | Gerar entidades, DTOs, controller e mapeamento do Entity Framework para módulos e tópicos, e a configuração de CORS |
| Banco de dados | Escrever scripts SQL de módulos e tópicos e orientar o uso de um banco de código aberto |
| Depuração | Diagnosticar erros do Angular, do .NET, do SQL e do Git, a partir de mensagens e capturas de tela |
| Revisão | Comparar o repositório com o enunciado e listar o que faltava |
| Qualidade e entrega | Corrigir pontos de responsividade, fonte, rotas e acessibilidade; sugerir padrão de mensagens de *commit*; redigir o `README.md` e este arquivo |
| Conteúdo | Gerar ilustrações provisórias (SVG) para as imagens de apresentação dos cursos |

## 3. Prompts relevantes

| # | Prompt (resumo ou trecho) | O que a IA entregou | Aproveitado ou descartado |
|---|---|---|---|
| 1 | "Ajuste o código para o H1 e o p para estar nesta disposição" (com captura do Figma) | CSS com `flex` para número à esquerda e texto à direita | **Aproveitado**, com várias rodadas de ajuste fino comparando com novas capturas |
| 2 | "me ajude" (com captura do erro `'imports' must be an array… (-991010)` no `FontAwesomeModule`) | Causas prováveis e passos de correção | **Aproveitado após correção** (ver erro 1, na seção 5) |
| 3 | "me ajude a integrar o back com o front" (com o `CursosController.cs`) | CORS, modelos TypeScript, serviço e componente que consomem `GET /api/cursos` | **Aproveitado com correções**: os nomes dos campos estavam errados (ver seção 5) |
| 4 | "faça o código do back end modulo e topico" | Entidades, DTOs, controller e mapeamento do `AppDbContext` | **Aproveitado**; os nomes dos arquivos de DTO foram ajustados para evitar duplicidade |
| 5 | "analise o front e compare o que falta" (com o PDF e o código) | Relatório de lacunas em relação ao enunciado | **Aproveitado** como lista de pendências |
| 6 | "avalie o que foi feito e me diga o que falta fazer" (com o repositório completo) | Avaliação por requisito, com prioridades | **Aproveitado** como lista de pendências |
| 7 | "Crie a documentação, README.md e USO_DE_IA.md" (com o enunciado) | Este arquivo e o `README.md` | **Aproveitado**, com revisão do conteúdo |

Também foram **descartadas**:

- a sugestão de montar a URL das imagens com o endereço da API (as imagens ficam na pasta `public` do Angular);
- os scripts de banco para SQL Server: o enunciado exige banco de código aberto, e o projeto usa MySQL.

## 4. Como o conteúdo foi validado

- **Execução real:** compilação e execução da API (`dotnet build` e `dotnet run`) e do front (`ng serve`); os erros foram lidos no terminal e no console do navegador e devolvidos à IA para correção.
- **Contrato da API:** abertura de `http://localhost:5000/api/cursos` e de `/api/cursos/{id}/modulos` no navegador, comparando o JSON real com os modelos TypeScript.
- **Banco:** consultas `SELECT` no cliente SQL para conferir os dados cadastrados.
- **Visual:** comparação das telas com as capturas do Figma.
- **Informações técnicas externas:** a compatibilidade de versões de pacotes foi verificada por **pesquisa na documentação oficial**, e não pela memória do modelo (ver erro 1).
- **Verificação feita pela própria IA, em ambiente isolado:** compilação do front-end (`ng build`) e teste do servidor SSR com uma API simulada, cobrindo `/curso/1` (curso existente), `/curso/99` (curso inexistente), `/curso/500` (falha da API), `/curso/abc` (id inválido), `/area-aluno` e uma rota inexistente. Esse teste **não substitui** a execução local com a API e o banco reais.
- **Revisão:** o código foi lido antes de ser aceito; nenhum dado real foi usado (todos os dados são fictícios).
- **O que não foi validado:** não há testes automatizados no projeto, e a suíte `ng test` não foi executada.

## 5. Exemplo de erro ou inadequação identificado

### Erro principal: nomes de campos supostos pela IA

**O que aconteceu.** Ao integrar o carrossel de cursos à API, a IA escreveu os modelos TypeScript sem ter visto o `CursoResponse` do back-end. Ela avisou que os nomes eram uma suposição e usou `itens`, `idCursos`, `statusNome`, `modalidadeNome` e `categoriaNome`.

**Como foi identificado.** A página mostrou apenas o card fixo "Todos os cursos": a lista de cursos estava vazia. Ao abrir `http://localhost:5000/api/cursos` no navegador, o JSON real trazia `items`, `id`, `status`, `modalidade` e `categoria`.

**Correção realizada.** O modelo (`curso.ts`) e o componente foram ajustados aos nomes reais (`res.items`, `curso.id`, `curso.status`, `curso.modalidade`, `curso.categoria`), e o `track` e o `routerLink` passaram a usar `curso.id`. A lição aplicada nas etapas seguintes: **conferir o contrato com o JSON real antes de escrever o código que o consome**.

### Outros erros identificados e corrigidos

| # | Erro ou inadequação | Como foi identificado | Correção |
|---|---|---|---|
| 1 | A tabela de compatibilidade do `@fortawesome/angular-fontawesome` com o Angular, dada pela IA, estava desatualizada | O erro `-991010` persistiu com Angular CLI 20.3 e `angular-fontawesome` 5.1.0 | A IA pesquisou a documentação oficial: a série 5.x é para Angular 22 e o Angular 20 usa a série 3.x. Foi instalada a versão `3` (o `package.json` usa `^3.0.0`) |
| 2 | `INSERT` de tópicos com `SCOPE_IDENTITY()` e ordem de colunas supostas | Erro 8101 do SQL Server | Reescrito com lista explícita de colunas e variável para o id do módulo |
| 3 | A IA assumiu que as imagens dos cursos eram servidas pela API | O candidato informou que ficam na pasta `public` do Angular | Removido o prefixo com o endereço da API |
| 4 | Testes sugeridos com `fakeAsync` | A análise do `package.json` mostrou que o projeto é *zoneless* e não tem `zone.js` | Testes descartados |
| 5 | Arquivo de DTO duplicado (`CursoDtos.cs` e `CursosDto.cs`) após a restauração de arquivos | Erro `CS8863` e busca por `record CursoResponse` com `grep` | Arquivo duplicado removido |
| 6 | Rota "não encontrada" criada pela IA não funcionava ao digitar um endereço inexistente direto na barra | Teste do servidor SSR feito pela IA: resposta "Cannot GET" | A regra de renderização do curinga em `app.routes.server.ts` foi alterada; o teste passou a mostrar a página 404 |

## 6. Responsabilidade e limites

- Todo o código gerado por IA foi lido, executado e ajustado pelo candidato, e que as decisões de arquitetura e a escolha do banco foram do candidato.
- A IA foi usada como apoio. A conferência do resultado (execução, comparação com o Figma e com o enunciado) é de responsabilidade do candidato.
- Nenhum dado pessoal ou real foi enviado à ferramenta; todos os dados do projeto são fictícios.

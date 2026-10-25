-- =====================================================================
-- SESI Cursos - Dados fictícios (MySQL 8+ / MariaDB 10.5+)
-- Execute DEPOIS de sesi_cursos.sql.
-- ATENÇÃO: este script apaga e recria os dados de exemplo; pode ser
-- executado novamente quando quiser voltar ao estado inicial.
-- Conteúdo: 7 cursos, 4 categorias, 3 modalidades e 3 status.
-- =====================================================================
SET NAMES utf8mb4;
USE sesi_cursos;

SET FOREIGN_KEY_CHECKS = 0;
TRUNCATE TABLE topicos;
TRUNCATE TABLE modulos;
TRUNCATE TABLE cursos;
TRUNCATE TABLE status;
TRUNCATE TABLE modalidades;
TRUNCATE TABLE categorias;
SET FOREIGN_KEY_CHECKS = 1;

INSERT INTO categorias (categoria) VALUES
    ('Matemática'), ('Ciências da Natureza'), ('Ciências Humanas'), ('Linguagens');

INSERT INTO modalidades (modalidade) VALUES
    ('Presencial'), ('Híbrido'), ('EAD');

INSERT INTO status (status) VALUES
    ('Ativo'), ('Inativo'), ('Em breve');


-- ==========================================
-- CURSO 1: Sustentabilidade Industrial
-- ==========================================
INSERT INTO cursos (nome_completo, nome_breve, descricao, carga_horaria, publico_alvo, imagem_apresentacao, data_disponibilizacao, fk_id_status, fk_id_modalidade, fk_id_categoria)
VALUES (
    'Introdução à Sustentabilidade e Gestão de Resíduos Industriais',
    'Sustentabilidade Industrial',
    'Aprenda práticas essenciais para a gestão ambiental, a redução de resíduos e a eficiência energética no contexto industrial.',
    30,
    'Técnicos, gestores ambientais e profissionais da indústria.',
    '/assets/cursos/sustentabilidade.svg',
    '2026-04-01',
    (SELECT id_status FROM status WHERE status = 'Ativo'),
    (SELECT id_modalidade FROM modalidades WHERE modalidade = 'Híbrido'),
    (SELECT id_categoria FROM categorias WHERE categoria = 'Ciências da Natureza')
);
SET @curso = LAST_INSERT_ID();

INSERT INTO modulos (titulo, ordem, fk_id_cursos) VALUES ('Fundamentos de Gestão Ambiental', 1, @curso);
SET @modulo = LAST_INSERT_ID();
INSERT INTO topicos (descricao, ordem, fk_id_modulos) VALUES
    ('Conceitos básicos de desenvolvimento sustentável na indústria.', 1, @modulo),
    ('Identificação de impactos ambientais das operações.', 2, @modulo);

INSERT INTO modulos (titulo, ordem, fk_id_cursos) VALUES ('Gestão e Tratamento de Resíduos', 2, @curso);
SET @modulo = LAST_INSERT_ID();
INSERT INTO topicos (descricao, ordem, fk_id_modulos) VALUES
    ('Classificação e armazenamento seguro de resíduos industriais.', 1, @modulo),
    ('Processos de reciclagem e destinação final adequada.', 2, @modulo);


-- ==========================================
-- CURSO 2: Comunicação Profissional
-- ==========================================
INSERT INTO cursos (nome_completo, nome_breve, descricao, carga_horaria, publico_alvo, imagem_apresentacao, data_disponibilizacao, fk_id_status, fk_id_modalidade, fk_id_categoria)
VALUES (
    'Comunicação Assertiva e Escrita Técnica no Ambiente Profissional',
    'Comunicação Profissional',
    'Desenvolva competências de comunicação escrita e verbal para relatórios, e-mails e apresentações corporativas.',
    20,
    'Colaboradores de todos os setores, líderes e equipes administrativas.',
    '/assets/cursos/comunicacao.svg',
    '2026-04-15',
    (SELECT id_status FROM status WHERE status = 'Ativo'),
    (SELECT id_modalidade FROM modalidades WHERE modalidade = 'EAD'),
    (SELECT id_categoria FROM categorias WHERE categoria = 'Linguagens')
);
SET @curso = LAST_INSERT_ID();

INSERT INTO modulos (titulo, ordem, fk_id_cursos) VALUES ('Princípios da Comunicação Assertiva', 1, @curso);
SET @modulo = LAST_INSERT_ID();
INSERT INTO topicos (descricao, ordem, fk_id_modulos) VALUES
    ('A importância da clareza e da objetividade na mensagem.', 1, @modulo),
    ('Técnicas de escuta ativa e feedback construtivo.', 2, @modulo);

INSERT INTO modulos (titulo, ordem, fk_id_cursos) VALUES ('Redação de Documentos Técnicos', 2, @curso);
SET @modulo = LAST_INSERT_ID();
INSERT INTO topicos (descricao, ordem, fk_id_modulos) VALUES
    ('Estruturação de relatórios, memorandos e atas de reunião.', 1, @modulo),
    ('Correção gramatical e adequação do tom corporativo.', 2, @modulo);


-- ==========================================
-- CURSO 3: Liderança e Equipes
-- ==========================================
INSERT INTO cursos (nome_completo, nome_breve, descricao, carga_horaria, publico_alvo, imagem_apresentacao, data_disponibilizacao, fk_id_status, fk_id_modalidade, fk_id_categoria)
VALUES (
    'Liderança e Gestão de Equipes de Alto Desempenho',
    'Liderança e Equipes',
    'Capacite líderes para motivar equipes, gerir conflitos e otimizar a produtividade com foco em resultados sustentáveis.',
    50,
    'Supervisores, gestores de projeto e líderes de equipe.',
    '/assets/cursos/lideranca.svg',
    '2026-05-01',
    (SELECT id_status FROM status WHERE status = 'Ativo'),
    (SELECT id_modalidade FROM modalidades WHERE modalidade = 'Presencial'),
    (SELECT id_categoria FROM categorias WHERE categoria = 'Ciências Humanas')
);
SET @curso = LAST_INSERT_ID();

INSERT INTO modulos (titulo, ordem, fk_id_cursos) VALUES ('Fundamentos da Liderança Moderna', 1, @curso);
SET @modulo = LAST_INSERT_ID();
INSERT INTO topicos (descricao, ordem, fk_id_modulos) VALUES
    ('Estilos de liderança e o papel do líder contemporâneo.', 1, @modulo),
    ('Desenvolvimento da inteligência emocional na gestão.', 2, @modulo);

INSERT INTO modulos (titulo, ordem, fk_id_cursos) VALUES ('Gestão de Conflitos e Motivação', 2, @curso);
SET @modulo = LAST_INSERT_ID();
INSERT INTO topicos (descricao, ordem, fk_id_modulos) VALUES
    ('Estratégias para a resolução pacífica de conflitos no trabalho.', 1, @modulo),
    ('Ferramentas práticas para manter a motivação e o engajamento da equipe.', 2, @modulo);

INSERT INTO modulos (titulo, ordem, fk_id_cursos) VALUES ('Metas e Acompanhamento de Resultados', 3, @curso);
SET @modulo = LAST_INSERT_ID();
INSERT INTO topicos (descricao, ordem, fk_id_modulos) VALUES
    ('Definição de metas claras e indicadores de desempenho.', 1, @modulo),
    ('Conversas de acompanhamento e plano de desenvolvimento individual.', 2, @modulo);


-- ==========================================
-- CURSO 4: Matemática Industrial
-- ==========================================
INSERT INTO cursos (nome_completo, nome_breve, descricao, carga_horaria, publico_alvo, imagem_apresentacao, data_disponibilizacao, fk_id_status, fk_id_modalidade, fk_id_categoria)
VALUES (
    'Matemática Aplicada aos Processos Industriais',
    'Matemática Industrial',
    'Revise e aplique conceitos de proporção, estatística e cálculo de custos em situações reais da rotina de produção.',
    60,
    'Operadores, auxiliares de produção e estudantes de cursos técnicos.',
    '/assets/cursos/matematica.svg',
    '2026-11-16',
    (SELECT id_status FROM status WHERE status = 'Em breve'),
    (SELECT id_modalidade FROM modalidades WHERE modalidade = 'Presencial'),
    (SELECT id_categoria FROM categorias WHERE categoria = 'Matemática')
);
SET @curso = LAST_INSERT_ID();

INSERT INTO modulos (titulo, ordem, fk_id_cursos) VALUES ('Proporções e Regra de Três na Produção', 1, @curso);
SET @modulo = LAST_INSERT_ID();
INSERT INTO topicos (descricao, ordem, fk_id_modulos) VALUES
    ('Razão, proporção e escalas em receitas e ordens de produção.', 1, @modulo),
    ('Regra de três simples e composta aplicada ao dia a dia da fábrica.', 2, @modulo);

INSERT INTO modulos (titulo, ordem, fk_id_cursos) VALUES ('Estatística Básica e Controle de Qualidade', 2, @curso);
SET @modulo = LAST_INSERT_ID();
INSERT INTO topicos (descricao, ordem, fk_id_modulos) VALUES
    ('Média, mediana e desvio padrão de medições.', 1, @modulo),
    ('Leitura de gráficos de controle de processos.', 2, @modulo);

INSERT INTO modulos (titulo, ordem, fk_id_cursos) VALUES ('Custos, Rendimento e Perdas', 3, @curso);
SET @modulo = LAST_INSERT_ID();
INSERT INTO topicos (descricao, ordem, fk_id_modulos) VALUES
    ('Cálculo de custo unitário e margem.', 1, @modulo),
    ('Indicadores de rendimento e desperdício.', 2, @modulo);


-- ==========================================
-- CURSO 5: Segurança do Trabalho
-- ==========================================
INSERT INTO cursos (nome_completo, nome_breve, descricao, carga_horaria, publico_alvo, imagem_apresentacao, data_disponibilizacao, fk_id_status, fk_id_modalidade, fk_id_categoria)
VALUES (
    'Segurança do Trabalho e Prevenção de Acidentes',
    'Segurança do Trabalho',
    'Conheça as normas e as boas práticas para identificar riscos, usar equipamentos de proteção e prevenir acidentes no ambiente industrial.',
    40,
    'Trabalhadores da indústria, brigadistas e membros de comissões internas de prevenção.',
    '/assets/cursos/seguranca.svg',
    '2027-02-01',
    (SELECT id_status FROM status WHERE status = 'Em breve'),
    (SELECT id_modalidade FROM modalidades WHERE modalidade = 'Híbrido'),
    (SELECT id_categoria FROM categorias WHERE categoria = 'Ciências da Natureza')
);
SET @curso = LAST_INSERT_ID();

INSERT INTO modulos (titulo, ordem, fk_id_cursos) VALUES ('Riscos Ocupacionais', 1, @curso);
SET @modulo = LAST_INSERT_ID();
INSERT INTO topicos (descricao, ordem, fk_id_modulos) VALUES
    ('Tipos de riscos: físicos, químicos, biológicos e ergonômicos.', 1, @modulo),
    ('Elaboração de mapas de risco do setor.', 2, @modulo);

INSERT INTO modulos (titulo, ordem, fk_id_cursos) VALUES ('Equipamentos de Proteção', 2, @curso);
SET @modulo = LAST_INSERT_ID();
INSERT INTO topicos (descricao, ordem, fk_id_modulos) VALUES
    ('Escolha, uso correto e conservação de EPIs.', 1, @modulo),
    ('Proteções coletivas e sinalização de segurança.', 2, @modulo);

INSERT INTO modulos (titulo, ordem, fk_id_cursos) VALUES ('Emergências e Primeiros Socorros', 3, @curso);
SET @modulo = LAST_INSERT_ID();
INSERT INTO topicos (descricao, ordem, fk_id_modulos) VALUES
    ('Planos de evacuação e combate a princípio de incêndio.', 1, @modulo),
    ('Noções básicas de primeiros socorros no ambiente de trabalho.', 2, @modulo);


-- ==========================================
-- CURSO 6: Inglês Técnico
-- ==========================================
INSERT INTO cursos (nome_completo, nome_breve, descricao, carga_horaria, publico_alvo, imagem_apresentacao, data_disponibilizacao, fk_id_status, fk_id_modalidade, fk_id_categoria)
VALUES (
    'Inglês Técnico para a Indústria',
    'Inglês Técnico',
    'Amplie o vocabulário técnico em inglês para ler manuais, atender fornecedores internacionais e participar de reuniões globais.',
    80,
    'Profissionais de manutenção, qualidade, compras e engenharia.',
    '/assets/cursos/ingles.svg',
    '2025-08-01',
    (SELECT id_status FROM status WHERE status = 'Inativo'),
    (SELECT id_modalidade FROM modalidades WHERE modalidade = 'EAD'),
    (SELECT id_categoria FROM categorias WHERE categoria = 'Linguagens')
);
SET @curso = LAST_INSERT_ID();

INSERT INTO modulos (titulo, ordem, fk_id_cursos) VALUES ('Vocabulário de Fábrica e Manutenção', 1, @curso);
SET @modulo = LAST_INSERT_ID();
INSERT INTO topicos (descricao, ordem, fk_id_modulos) VALUES
    ('Nomes de máquinas, ferramentas e peças em inglês.', 1, @modulo),
    ('Leitura de manuais e fichas técnicas.', 2, @modulo);

INSERT INTO modulos (titulo, ordem, fk_id_cursos) VALUES ('Comunicação com Fornecedores', 2, @curso);
SET @modulo = LAST_INSERT_ID();
INSERT INTO topicos (descricao, ordem, fk_id_modulos) VALUES
    ('Escrita de e-mails e pedidos de cotação.', 1, @modulo),
    ('Condução de ligações e reuniões em inglês.', 2, @modulo);


-- ==========================================
-- CURSO 7: Ética e Cidadania
-- ==========================================
INSERT INTO cursos (nome_completo, nome_breve, descricao, carga_horaria, publico_alvo, imagem_apresentacao, data_disponibilizacao, fk_id_status, fk_id_modalidade, fk_id_categoria)
VALUES (
    'Ética e Cidadania no Mundo do Trabalho',
    'Ética e Cidadania',
    'Reflita sobre direitos, deveres, diversidade e responsabilidade social nas relações profissionais.',
    25,
    'Jovens aprendizes, colaboradores em início de carreira e equipes de recursos humanos.',
    '/assets/cursos/etica.svg',
    NULL,
    (SELECT id_status FROM status WHERE status = 'Inativo'),
    (SELECT id_modalidade FROM modalidades WHERE modalidade = 'Híbrido'),
    (SELECT id_categoria FROM categorias WHERE categoria = 'Ciências Humanas')
);
SET @curso = LAST_INSERT_ID();

INSERT INTO modulos (titulo, ordem, fk_id_cursos) VALUES ('Direitos e Deveres do Trabalhador', 1, @curso);
SET @modulo = LAST_INSERT_ID();
INSERT INTO topicos (descricao, ordem, fk_id_modulos) VALUES
    ('Princípios básicos da legislação trabalhista.', 1, @modulo),
    ('Postura profissional e responsabilidade individual.', 2, @modulo);

INSERT INTO modulos (titulo, ordem, fk_id_cursos) VALUES ('Diversidade e Respeito no Trabalho', 2, @curso);
SET @modulo = LAST_INSERT_ID();
INSERT INTO topicos (descricao, ordem, fk_id_modulos) VALUES
    ('Convivência com diferenças e combate ao preconceito.', 1, @modulo),
    ('Boas práticas de inclusão nas equipes.', 2, @modulo);

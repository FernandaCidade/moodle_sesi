use sesi_cursos

SELECT * FROM cursos

INSERT INTO cursos (nome_completo, nome_breve, descricao, carga_horaria, publico_alvo, imagem_apresentacao, data_disponibilizacao, fk_id_status, fk_id_modalidade, fk_id_categoria)
VALUES (
    'Introdução à Sustentabilidade e Gestão de Resíduos Industriais', 
    'Sustentabilidade Industrial', 
    'Aprenda práticas essenciais para a gestão ambiental, redução de resíduos e eficiência energética no contexto industrial.', 
    30, 
    'Técnicos, gestores ambientais e profissionais da indústria.', 
    'sustentabilidade.png', 
    '2026-04-01', 
    (SELECT id_status FROM status WHERE status = 'Ativo'), 
    (SELECT id_modalidade FROM modalidades WHERE modalidade = 'Híbrido'), 
    (SELECT id_categoria FROM categorias WHERE categoria = 'Ciências da Natureza')
);

DECLARE @id_curso1 INT = SCOPE_IDENTITY();
DECLARE @m INT;

INSERT INTO modulos (titulo, ordem, fk_id_cursos) VALUES ('Fundamentos de Gestão Ambiental', 1, @id_curso1);
SET @m = SCOPE_IDENTITY();
INSERT INTO topicos (descricao, ordem, fk_id_modulos) VALUES
('Conceitos básicos de desenvolvimento sustentável na indústria.', 1, @m),
('Identificação de impactes ambientais das operações.', 2, @m);

INSERT INTO modulos (titulo, ordem, fk_id_cursos) VALUES ('Gestão e Tratamento de Resíduos', 2, @id_curso1);
SET @m = SCOPE_IDENTITY();
INSERT INTO topicos (descricao, ordem, fk_id_modulos) VALUES
('Classificação e armazenamento seguro de resíduos industriais.', 1, @m),
('Processos de reciclagem e destinação final adequada.', 2, @m);


-- ==========================================
-- CURSO 2: Comunicação Profissional
-- ==========================================
INSERT INTO cursos (nome_completo, nome_breve, descricao, carga_horaria, publico_alvo, imagem_apresentacao, data_disponibilizacao, fk_id_status, fk_id_modalidade, fk_id_categoria)
VALUES (
    'Comunicação Assertiva e Escrita Técnica no Ambiente Profissional', 
    'Comunicação Profissional', 
    'Desenvolva competências avançadas de comunicação escrita e verbal para relatórios, e-mails e apresentações corporativas.', 
    20, 
    'Colaboradores de todos os setores, líderes e equipas administrativas.', 
    'comunicacao.png', 
    '2026-04-15', 
    (SELECT id_status FROM status WHERE status = 'Ativo'), 
    (SELECT id_modalidade FROM modalidades WHERE modalidade = 'EAD'), 
    (SELECT id_categoria FROM categorias WHERE categoria = 'Linguagens')
);

DECLARE @id_curso2 INT = SCOPE_IDENTITY();

INSERT INTO modulos (titulo, ordem, fk_id_cursos) VALUES ('Princípios da Comunicação Assertiva', 1, @id_curso2);
SET @m = SCOPE_IDENTITY();
INSERT INTO topicos (descricao, ordem, fk_id_modulos) VALUES
('A importância da clareza e objetividade na mensagem.', 1, @m),
('Técnicas de escuta ativa e feedback construtivo.', 2, @m);

INSERT INTO modulos (titulo, ordem, fk_id_cursos) VALUES ('Redação de Documentos Técnicos', 2, @id_curso2);
SET @m = SCOPE_IDENTITY();
INSERT INTO topicos (descricao, ordem, fk_id_modulos) VALUES
('Estruturação de relatórios, memorandos e atas de reuniões.', 1, @m),
('Correção gramatical e adequação do tom corporativo.', 2, @m);


-- ==========================================
-- CURSO 3: Liderança e Equipas
-- ==========================================
INSERT INTO cursos (nome_completo, nome_breve, descricao, carga_horaria, publico_alvo, imagem_apresentacao, data_disponibilizacao, fk_id_status, fk_id_modalidade, fk_id_categoria)
VALUES (
    'Liderança e Gestão de Equipas de Alto Desempenho', 
    'Liderança e Equipas', 
    'Capacite líderes para motivar equipas, gerir conflitos e otimizar a produtividade com foco em resultados sustentáveis.', 
    50, 
    'Supervisores, gestores de projeto e líderes de equipa.', 
    'lideranca.png', 
    '2026-05-01', 
    (SELECT id_status FROM status WHERE status = 'Ativo'), 
    (SELECT id_modalidade FROM modalidades WHERE modalidade = 'Presencial'), 
    (SELECT id_categoria FROM categorias WHERE categoria = 'Ciências Humanas')
);

DECLARE @id_curso3 INT = SCOPE_IDENTITY();

INSERT INTO modulos (titulo, ordem, fk_id_cursos) VALUES ('Fundamentos da Liderança Moderna', 1, @id_curso3);
SET @m = SCOPE_IDENTITY();
INSERT INTO topicos (descricao, ordem, fk_id_modulos) VALUES
('Estilos de liderança e o papel do líder contemporâneo.', 1, @m),
('Desenvolvimento da inteligência emocional na gestão.', 2, @m);

INSERT INTO modulos (titulo, ordem, fk_id_cursos) VALUES ('Gestão de Conflitos e Motivação', 2, @id_curso3);
SET @m = SCOPE_IDENTITY();
INSERT INTO topicos (descricao, ordem, fk_id_modulos) VALUES
('Estratégias para resolução pacífica de conflitos no local de trabalho.', 1, @m),
('Ferramentas práticas para manter a motivação e o engajamento da equipa.', 2, @m);
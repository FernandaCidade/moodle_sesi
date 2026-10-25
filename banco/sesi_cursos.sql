-- =====================================================================
-- SESI Cursos - Estrutura do banco (MySQL 8+ / MariaDB 10.5+)
-- Execute ESTE arquivo primeiro e, em seguida, dados_cursos.sql
-- Pode ser executado mais de uma vez (não apaga dados existentes).
-- =====================================================================
SET NAMES utf8mb4;

CREATE DATABASE IF NOT EXISTS sesi_cursos
    CHARACTER SET utf8mb4
    COLLATE utf8mb4_unicode_ci;

USE sesi_cursos;

-- ---------- Tabelas de apoio ----------
CREATE TABLE IF NOT EXISTS categorias (
    id_categoria INT NOT NULL AUTO_INCREMENT,
    categoria    VARCHAR(80) NOT NULL,
    PRIMARY KEY (id_categoria),
    UNIQUE KEY uq_categorias_categoria (categoria)
);

CREATE TABLE IF NOT EXISTS modalidades (
    id_modalidade INT NOT NULL AUTO_INCREMENT,
    modalidade    VARCHAR(30) NOT NULL,
    PRIMARY KEY (id_modalidade),
    UNIQUE KEY uq_modalidades_modalidade (modalidade)
);

CREATE TABLE IF NOT EXISTS status (
    id_status INT NOT NULL AUTO_INCREMENT,
    status    VARCHAR(20) NOT NULL,
    PRIMARY KEY (id_status),
    UNIQUE KEY uq_status_status (status)
);

-- ---------- Cursos ----------
CREATE TABLE IF NOT EXISTS cursos (
    id_cursos            INT NOT NULL AUTO_INCREMENT,
    nome_completo        VARCHAR(200) NOT NULL,
    nome_breve           VARCHAR(50)  NOT NULL,
    descricao            TEXT         NOT NULL,
    carga_horaria        INT          NOT NULL,
    publico_alvo         VARCHAR(255) NOT NULL,
    imagem_apresentacao  VARCHAR(255) NOT NULL,
    data_disponibilizacao DATE        NULL,
    fk_id_status         INT NULL,
    fk_id_modalidade     INT NULL,
    fk_id_categoria      INT NULL,
    PRIMARY KEY (id_cursos),
    KEY ix_cursos_nome_completo (nome_completo),
    CONSTRAINT fk_id_status     FOREIGN KEY (fk_id_status)     REFERENCES status (id_status),
    CONSTRAINT fk_id_modalidade FOREIGN KEY (fk_id_modalidade) REFERENCES modalidades (id_modalidade),
    CONSTRAINT fk_id_categoria  FOREIGN KEY (fk_id_categoria)  REFERENCES categorias (id_categoria)
);

-- ---------- Módulos e tópicos (apagados em cascata com o curso) ----------
CREATE TABLE IF NOT EXISTS modulos (
    id_modulos   INT NOT NULL AUTO_INCREMENT,
    titulo       VARCHAR(200) NOT NULL,
    ordem        INT NOT NULL,
    fk_id_cursos INT NOT NULL,
    PRIMARY KEY (id_modulos),
    KEY ix_modulos_curso (fk_id_cursos, ordem),
    CONSTRAINT fk_modulos_curso FOREIGN KEY (fk_id_cursos)
        REFERENCES cursos (id_cursos) ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS topicos (
    id_topicos   INT NOT NULL AUTO_INCREMENT,
    descricao    VARCHAR(500) NOT NULL,
    ordem        INT NOT NULL,
    fk_id_modulos INT NOT NULL,
    PRIMARY KEY (id_topicos),
    KEY ix_topicos_modulo (fk_id_modulos, ordem),
    CONSTRAINT fk_topicos_modulo FOREIGN KEY (fk_id_modulos)
        REFERENCES modulos (id_modulos) ON DELETE CASCADE
);

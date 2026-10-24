use sesi_cursos;

-- Criando tabelas
create table cursos(
	id_cursos int identity(1,1)primary key, 
	nome_completo varchar(200) not null, 
	nome_breve varchar(50) not null,
	descricao text not null,
	carga_horaria int not null,
	publico_alvo varchar(255) not null,
	imagem_apresentacao varchar(255) not null,
	data_disponibilizacao date null
);

create table status(
	id_status int identity(1,1) primary key,
	status varchar(20) not null
);

create table modalidades(
	id_modalidade int identity(1,1) primary key,
	modalidade varchar(30) unique not null
);

create table categorias(
	id_categoria int identity(1,1) primary key,
	categoria varchar(80)unique not null

);

--Inserindo chaves estrangeiras

alter table cursos add fk_id_status int;

alter table cursos add fk_id_modalidade int;

alter table cursos add fk_id_categoria int;

ALTER TABLE cursos 
ADD CONSTRAINT fk_id_status
FOREIGN KEY (fk_id_status) 
REFERENCES status(id_status);


ALTER TABLE cursos 
ADD CONSTRAINT fk_id_modalidade
FOREIGN KEY (fk_id_modalidade) 
REFERENCES modalidades(id_modalidade);


ALTER TABLE cursos 
ADD CONSTRAINT fk_id_categoria
FOREIGN KEY (fk_id_categoria) 
REFERENCES categorias(id_categoria);


--Inserindo valores nas tabelas

insert into categorias values('Matemática'),('Ciências da Natureza'), ('Ciências Humanas'),('Linguagens');
insert into modalidades values ('Presencial'), ('Híbrido'), ('EAD');


select * from cursos

select * from status


UPDATE cursos
SET imagem_apresentacao = '/assets/banner-ava.png'
WHERE id_cursos = 11;
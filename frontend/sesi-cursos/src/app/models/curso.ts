export interface Curso {
  id: number;
  nomeCompleto: string;
  nomeBreve: string;
  descricao: string;
  cargaHoraria: number;
  publicoAlvo: string;
  imagemApresentacao: string;
  dataDisponibilizacao: string | null;
  statusId: number | null;
  status: string | null;
  modalidadeId: number | null;
  modalidade: string | null;
  categoriaId: number | null;
  categoria: string | null;
}

export interface PagedResult<T> {
  page: number;
  pageSize: number;
  total: number;
  items: T[];
}

export interface Modulo {
  id: number;
  titulo: string;
  ordem: number;
  topicos: string[];
}
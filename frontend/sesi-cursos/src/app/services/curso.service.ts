import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable, map, shareReplay } from 'rxjs';
import { Curso, PagedResult, Modulo, Opcao, OpcoesFiltro } from '../models/curso';

export interface FiltrosCurso {
    busca?: string;
    categoriaId?: number | null;
    modalidadeId?: number | null;
    statusId?: number | null;
    page?: number;
    pageSize?: number;
}

@Injectable({ providedIn: 'root' })
export class CursoService {
    private http = inject(HttpClient);

    // Troque pela URL da sua API (no deploy, mova para um arquivo de ambiente)
    private readonly url = 'http://localhost:5000/api/cursos';

    private opcoes$?: Observable<OpcoesFiltro>;

    listar(filtros: FiltrosCurso = {}): Observable<PagedResult<Curso>> {
        let params = new HttpParams();
        Object.entries(filtros).forEach(([chave, valor]) => {
            if (valor !== undefined && valor !== null && valor !== '') {
                params = params.set(chave, String(valor));
            }
        });
        return this.http.get<PagedResult<Curso>>(this.url, { params });
    }

    obter(id: number): Observable<Curso> {
        return this.http.get<Curso>(`${this.url}/${id}`);
    }

    modulos(cursoId: number): Observable<Modulo[]> {
        return this.http.get<Modulo[]>(`${this.url}/${cursoId}/modulos`);
    }

    /**
     * Opções dos filtros (categoria, modalidade e status).
     * Montadas a partir dos cursos já cadastrados, sem depender de endpoints
     * extras. Se a API tiver rotas próprias para isso, troque este método.
     */
    opcoesFiltro(): Observable<OpcoesFiltro> {
        this.opcoes$ ??= this.listar({ page: 1, pageSize: 100 }).pipe(
            map((res) => ({
                categorias: this.unicos(res.items.map((c) => [c.categoriaId, c.categoria])),
                modalidades: this.unicos(res.items.map((c) => [c.modalidadeId, c.modalidade])),
                status: this.unicos(res.items.map((c) => [c.statusId, c.status])),
            })),
            shareReplay({ bufferSize: 1, refCount: false }),
        );
        return this.opcoes$;
    }

    private unicos(pares: (number | string | null)[][]): Opcao[] {
        const mapa = new Map<number, string>();
        for (const [id, nome] of pares) {
            if (typeof id === 'number' && typeof nome === 'string' && nome) mapa.set(id, nome);
        }
        return [...mapa].map(([id, nome]) => ({ id, nome }))
            .sort((a, b) => a.nome.localeCompare(b.nome, 'pt-BR'));
    }
}

import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Curso, PagedResult, Modulo } from '../models/curso';


@Injectable({ providedIn: 'root' })
export class CursoService {
    private http = inject(HttpClient);

    // Troque pela URL da sua API (veja o passo 3)
    private readonly url = 'http://localhost:5000/api/cursos';

    listar(filtros: { busca?: string; categoriaId?: number; page?: number; pageSize?: number } = {}): Observable<PagedResult<Curso>> {
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
}
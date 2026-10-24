import { Component, DestroyRef, computed, inject, signal } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { ActivatedRoute, ParamMap, Router, RouterLink } from '@angular/router';
import { Subject, catchError, debounceTime, map, merge, of, switchMap, tap } from 'rxjs';
import { CursoService } from '../services/curso.service';
import { Curso, OpcoesFiltro } from '../models/curso';

interface FiltrosUrl {
  busca: string;
  categoria: number | null;
  modalidade: number | null;
  status: number | null;
  pagina: number;
}

type ChaveFiltro = 'categoria' | 'modalidade' | 'status';

@Component({
  selector: 'app-catalogo',
  imports: [RouterLink],
  templateUrl: './catalogo.html',
  styleUrl: './catalogo.css',
})
export class Catalogo {
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private service = inject(CursoService);
  private destroyRef = inject(DestroyRef);

  readonly tamanhoPagina = 9;
  readonly esqueleto = [1, 2, 3, 4, 5, 6];

  cursos = signal<Curso[]>([]);
  total = signal(0);
  carregando = signal(true);
  erro = signal(false);
  opcoes = signal<OpcoesFiltro>({ categorias: [], modalidades: [], status: [] });

  // A URL (query params) é a fonte da verdade dos filtros.
  filtros = signal<FiltrosUrl>({ busca: '', categoria: null, modalidade: null, status: null, pagina: 1 });
  termo = signal('');

  temFiltros = computed(() => {
    const f = this.filtros();
    return !!f.busca || f.categoria !== null || f.modalidade !== null || f.status !== null;
  });
  totalPaginas = computed(() => Math.max(1, Math.ceil(this.total() / this.tamanhoPagina)));

  private busca$ = new Subject<string>();
  private recarregar$ = new Subject<void>();
  private ultimaBuscaEnviada = '';

  constructor() {
    // Opções dos filtros
    this.service.opcoesFiltro()
      .pipe(catchError(() => of<OpcoesFiltro>({ categorias: [], modalidades: [], status: [] })), takeUntilDestroyed(this.destroyRef))
      .subscribe((o) => this.opcoes.set(o));

    // Pesquisa com atraso para não chamar a API a cada tecla
    this.busca$.pipe(debounceTime(350), takeUntilDestroyed(this.destroyRef))
      .subscribe((v) => this.aplicarBusca(v));

    // Carrega a lista sempre que a URL mudar (ou ao tentar novamente)
    merge(
      this.route.queryParamMap.pipe(map((p) => this.ler(p))),
      this.recarregar$.pipe(map(() => this.filtros())),
    ).pipe(
      tap((f) => {
        this.filtros.set(f);
        if (f.busca !== this.ultimaBuscaEnviada) {
          this.ultimaBuscaEnviada = f.busca;
          this.termo.set(f.busca);
        }
        this.carregando.set(true);
        this.erro.set(false);
      }),
      switchMap((f) =>
        this.service.listar({
          busca: f.busca,
          categoriaId: f.categoria,
          modalidadeId: f.modalidade,
          statusId: f.status,
          page: f.pagina,
          pageSize: this.tamanhoPagina,
        }).pipe(catchError(() => of(null))),
      ),
      takeUntilDestroyed(this.destroyRef),
    ).subscribe((res) => {
      this.carregando.set(false);
      if (!res) {
        this.erro.set(true);
        this.cursos.set([]);
        this.total.set(0);
        return;
      }
      // Página fora do intervalo (ex.: URL antiga): volta para a primeira
      if (res.items.length === 0 && res.total > 0 && this.filtros().pagina > 1) {
        this.irPara(1);
        return;
      }
      this.cursos.set(res.items);
      this.total.set(res.total);
    });
  }

  digitar(valor: string): void {
    this.termo.set(valor);
    this.busca$.next(valor);
  }

  enviarAgora(): void {
    this.aplicarBusca(this.termo());
  }

  escolher(chave: ChaveFiltro, valor: string): void {
    this.navegar({ [chave]: valor || null, pagina: null });
  }

  irPara(pagina: number): void {
    this.navegar({ pagina: pagina > 1 ? pagina : null });
  }

  limpar(): void {
    this.termo.set('');
    this.ultimaBuscaEnviada = '';
    this.navegar({ busca: null, categoria: null, modalidade: null, status: null, pagina: null });
  }

  recarregar(): void {
    this.recarregar$.next();
  }

  detalhes(c: Curso): string {
    return [c.modalidade, c.cargaHoraria ? `${c.cargaHoraria}h` : null, c.status]
      .filter(Boolean)
      .join(' · ');
  }

  private aplicarBusca(valor: string): void {
    const v = valor.trim();
    if (v === this.filtros().busca) return;
    this.ultimaBuscaEnviada = v;
    this.navegar({ busca: v || null, pagina: null });
  }

  private navegar(queryParams: Record<string, string | number | null>): void {
    this.router.navigate([], {
      relativeTo: this.route,
      queryParams,
      queryParamsHandling: 'merge',
    });
  }

  private ler(p: ParamMap): FiltrosUrl {
    const num = (chave: string): number | null => {
      const n = Number(p.get(chave));
      return Number.isInteger(n) && n > 0 ? n : null;
    };
    return {
      busca: (p.get('busca') ?? '').trim(),
      categoria: num('categoria'),
      modalidade: num('modalidade'),
      status: num('status'),
      pagina: num('pagina') ?? 1,
    };
  }
}

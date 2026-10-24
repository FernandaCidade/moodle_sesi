import { Component, ElementRef, ViewChild, OnInit, inject, signal } from '@angular/core';
import { RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { CursoService } from '../services/curso.service';
import { Curso } from '../models/curso';

@Component({
  selector: 'app-inicio',
  imports: [RouterLink, CommonModule],
  templateUrl: './inicio.html',
  styleUrl: './inicio.css',
})
export class Inicio implements OnInit {
  private cursoService = inject(CursoService);

  cursos = signal<Curso[]>([]);
  erro = signal('');

  @ViewChild('carouselGrid') carouselGrid!: ElementRef<HTMLDivElement>;

  ngOnInit(): void {
    this.cursoService.listar({ page: 1, pageSize: 6 }).subscribe({
      next: (res) => this.cursos.set(res.items),
      error: (e) => {
        console.error(e);
        this.erro.set('Não foi possível carregar os cursos.');
      },
    });
  }

  rolarParaContainer(elemento: HTMLElement) {
    elemento.scrollIntoView({
      behavior: 'smooth',
      block: 'start',
      inline: 'nearest',
    });
  }

  moverOpcao(direcao: 'esquerda' | 'direita'): void {
    if (!this.carouselGrid) return;

    const container = this.carouselGrid.nativeElement;
    const card = container.firstElementChild as HTMLElement | null;
    const gap = parseFloat(getComputedStyle(container).columnGap) || 0;
    const passo = card ? card.offsetWidth + gap : 340;

    container.scrollBy({
      left: direcao === 'esquerda' ? -passo : passo,
      behavior: 'smooth',
    });
  }
}
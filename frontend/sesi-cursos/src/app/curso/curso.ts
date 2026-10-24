import { Component, OnInit, inject, signal } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { DatePipe } from '@angular/common';
import { CursoService } from '../services/curso.service';
import { Curso as CursoModel, Modulo } from '../models/curso';   // apelido para não conflitar

@Component({
  selector: 'app-curso',
  imports: [RouterLink, DatePipe],
  templateUrl: './curso.html',
  styleUrl: './curso.css',
})
export class Curso implements OnInit {
  private route = inject(ActivatedRoute);
  private cursoService = inject(CursoService);

  curso = signal<CursoModel | null>(null);
  modulos = signal<Modulo[]>([]);
  erro = signal('');

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));

    this.cursoService.obter(id).subscribe({
      next: (c) => this.curso.set(c),
      error: () => this.erro.set('Curso não encontrado.'),
    });

    this.cursoService.modulos(id).subscribe({
      next: (m) => this.modulos.set(m),
      error: (e) => console.error(e),
    });
  }
}
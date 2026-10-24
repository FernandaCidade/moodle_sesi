import { Component, HostListener, inject, signal } from '@angular/core';
import { MatMenuModule } from '@angular/material/menu';
import { MatButtonModule } from '@angular/material/button';
import { MatToolbarModule } from '@angular/material/toolbar';
import { NavigationEnd, Router, RouterLink } from '@angular/router';
import { filter } from 'rxjs';

@Component({
  selector: 'app-menu',
  standalone: true,
  imports: [
    MatButtonModule,
    MatMenuModule,
    MatToolbarModule,
    RouterLink
  ],
  templateUrl: './menu.html',
  styleUrl: './menu.css',
})
export class Menu {
  private router = inject(Router);

  isScrolled = false;

  // Defina os caminhos das versões do logo
  logoPadrao = 'assets/logo-sesi-industria-white.svg';
  logoScroll = 'assets/logo-sesi-industria-primary.svg';
  logoCurso = 'assets/logo-curso.svg';   // troque pelo nome do arquivo da logo da página do curso

  // true quando a rota atual é a página de um curso (/curso/1, /curso/2...)
  paginaCurso = signal(false);

  constructor() {
    // valor inicial (caso a pessoa abra direto em /curso/1)
    this.paginaCurso.set(this.router.url.startsWith('/curso/'));

    // atualiza a cada troca de página
    this.router.events
      .pipe(filter((e): e is NavigationEnd => e instanceof NavigationEnd))
      .subscribe((e) =>
        this.paginaCurso.set(e.urlAfterRedirects.startsWith('/curso/'))
      );
  }

  // Escolhe qual logo exibir
  get logoAtual(): string {
    if (this.paginaCurso()) return this.logoCurso;
    return this.isScrolled ? this.logoScroll : this.logoPadrao;
  }

  @HostListener('window:scroll', [])
  onWindowScroll() {
    // Altera para true se rolar mais de 50px para baixo
    this.isScrolled = window.scrollY > 50;
  }
}
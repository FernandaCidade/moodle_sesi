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
    RouterLink,
    
  ],
  templateUrl: './menu.html',
  styleUrl: './menu.css',
})
export class Menu {
  private router = inject(Router);

  isScrolled = false;

  // Caminhos das logos
  logoPadrao = 'assets/logo-sesi-industria-white.svg';
  logoScroll = 'assets/logo-sesi-industria-primary.svg';
  logoCurso = 'assets/logo-sesi-industria-primary.svg';   // troque pelo nome do arquivo da logo da aba de cursos

  // true quando a rota é a listagem (/cursos) ou o detalhe (/curso/1)
  paginaCurso = signal(false);

  constructor() {
    // valor inicial (caso a pessoa abra a página direto pela URL)
    this.paginaCurso.set(this.ehPaginaCurso(this.router.url));

    // atualiza a cada troca de página
    this.router.events
      .pipe(filter((e): e is NavigationEnd => e instanceof NavigationEnd))
      .subscribe((e) => this.paginaCurso.set(this.ehPaginaCurso(e.urlAfterRedirects)));
  }

  private ehPaginaCurso(url: string): boolean {
    const caminho = url.split(/[?#]/)[0];   // ignora query string e âncora
    return caminho === '/catalogo' || caminho.startsWith('/curso/');
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
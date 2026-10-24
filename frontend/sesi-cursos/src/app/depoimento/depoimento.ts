import { Component, ElementRef, ViewChild, input } from '@angular/core';
import { RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { Chamada } from '../chamada/chamada';

@Component({
  selector: 'app-depoimento',
  standalone: true,
  imports: [RouterLink, CommonModule, FontAwesomeModule, Chamada],
  templateUrl: './depoimento.html',
  styleUrl: './depoimento.css',
})
export class Depoimento {

  tema = input<'escuro' | 'claro'>('escuro');

  // Pegamos a referência do elemento HTML com a hashtag #carouselGrid
  @ViewChild('carouselGrid') carouselGrid!: ElementRef<HTMLDivElement>;

  rolarParaContainer(elemento: HTMLElement) {
    elemento.scrollIntoView({
      behavior: 'smooth', // Transição suave (com animação)
      block: 'start',     // Alinha o topo do container no topo da tela
      inline: 'nearest'
    });
  }

  moverOpcao(direcao: 'esquerda' | 'direita'): void {
    if (!this.carouselGrid) return;

    const container = this.carouselGrid.nativeElement;

    // Pega a largura real do card + o gap, em vez de um valor fixo
    const card = container.firstElementChild as HTMLElement | null;
    const gap = parseFloat(getComputedStyle(container).columnGap) || 0;
    const passo = card ? card.offsetWidth + gap : 340;

    container.scrollBy({
      left: direcao === 'esquerda' ? -passo : passo,
      behavior: 'smooth',
    });
  }
}
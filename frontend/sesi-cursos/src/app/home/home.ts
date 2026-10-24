import { Component } from '@angular/core';
import { Inicio } from '../inicio/inicio';
import { Resultado } from '../resultado/resultado';
import { Depoimento } from '../depoimento/depoimento';
import { Chamada } from '../chamada/chamada';

@Component({
  selector: 'app-home',
  imports: [Inicio, Resultado, Depoimento, Chamada],
  templateUrl: './home.html',
  styleUrl: './home.css',
})
export class Home { }
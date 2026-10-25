import { Component } from '@angular/core';
import { Inicio } from '../inicio/inicio';
import { Resultado } from '../resultado/resultado';
import { Depoimento } from '../depoimento/depoimento';
import { Chamada } from '../chamada/chamada';
import { FaleConosco } from '../fale-conosco/fale-conosco';

@Component({
  selector: 'app-home',
  imports: [Inicio, Resultado, Depoimento, Chamada, FaleConosco],
  templateUrl: './home.html',
  styleUrl: './home.css',
})
export class Home { }
import { Component } from '@angular/core';
import { Inicio } from '../inicio/inicio';
import { Resultado } from '../resultado/resultado';
import { Depoimento } from '../depoimento/depoimento';

@Component({
  selector: 'app-home',
  imports: [Inicio, Resultado, Depoimento],
  templateUrl: './home.html',
  styleUrl: './home.css',
})
export class Home { }
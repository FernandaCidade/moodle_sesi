import { Routes } from '@angular/router';
import { Home } from './home/home';
import { Curso } from './curso/curso';
import { Catalogo } from './catalogo/catalogo';

export const routes: Routes = [
    { path: '', component: Home, title: 'Sesi - Portal' },
    { path: 'catalogo', component: Catalogo, title: 'Cursos - Sesi Portal' },
    { path: 'curso/:id', component: Curso, title: 'Detalhe do curso - Sesi Portal' },
];
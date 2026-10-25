import { Routes } from '@angular/router';
import { Home } from './home/home';
import { Curso } from './curso/curso';
import { Catalogo } from './catalogo/catalogo';
import { FaleConosco } from './fale-conosco/fale-conosco';

export const routes: Routes = [
    { path: '', component: Home, title: 'Sesi - Portal' },
    { path: 'catalogo', component: Catalogo, title: 'Cursos - Sesi Portal' },
    { path: 'curso/:id', component: Curso, title: 'Detalhe do curso - Sesi Portal' },
    { path: 'fale-conosco', component: FaleConosco, title: 'Fale conosco - Sesi Portal' },

];
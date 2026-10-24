import { Routes } from '@angular/router';
import { Home } from './home/home';
import { Curso } from './curso/curso';

export const routes: Routes = [
    { path: '', component: Home },
    { path: 'curso/:id', component: Curso },
];
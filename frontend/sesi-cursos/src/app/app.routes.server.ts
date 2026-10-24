import { RenderMode, ServerRoute } from '@angular/ssr';

export const serverRoutes: ServerRoute[] = [
  { path: '', renderMode: RenderMode.Client },
  { path: 'catalogo', renderMode: RenderMode.Client },
  { path: 'curso/:id', renderMode: RenderMode.Server },
  { path: '**', renderMode: RenderMode.Prerender },
];
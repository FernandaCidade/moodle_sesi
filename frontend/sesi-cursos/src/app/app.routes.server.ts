import { RenderMode, ServerRoute } from '@angular/ssr';

export const serverRoutes: ServerRoute[] = [
  { path: 'curso/:id', renderMode: RenderMode.Server },
  { path: '**', renderMode: RenderMode.Prerender },
];
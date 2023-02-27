import { Routes, RouterModule, PreloadAllModules } from '@angular/router';

const routes: Routes = [
  { path: 'app-config', loadChildren: () => import ('./app-config/app-config.module').then(m => m.AppConfigModule) },
  { path: '', redirectTo: 'paycheck-calculator', pathMatch: 'full' },
  { path: '**', redirectTo: 'paycheck-calculator', pathMatch: 'full' }
];

/**
 * Routing for application root.
 */
export const routing = RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules, relativeLinkResolution: 'legacy' });

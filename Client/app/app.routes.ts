import { Routes, RouterModule, PreloadAllModules } from '@angular/router';

const routes: Routes = [
  { path: '', redirectTo: 'paycheck-calculator', pathMatch: 'full' },
  { path: '**', redirectTo: 'paycheck-calculator', pathMatch: 'full' },
  { path: 'app-config', loadChildren: './app-config/app-config.module#AppConfigModule' }
];

/**
 * Routing for application root.
 */
export const routing = RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules });

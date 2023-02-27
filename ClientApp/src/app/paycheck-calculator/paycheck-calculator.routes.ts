import { Routes, RouterModule, PreloadAllModules } from '@angular/router';
import { PaycheckCalculatorComponent } from './paycheck-calculator/paycheck-calculator.component';

const routes: Routes = [
    { path: 'paycheck-calculator', component: PaycheckCalculatorComponent }
];

export const routing = RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules, relativeLinkResolution: 'legacy' });

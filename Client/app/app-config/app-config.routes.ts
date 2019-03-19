import { Routes, RouterModule } from '@angular/router';

import { AppConfigComponent } from './app-config.component';

const routes: Routes = [
    { path: '', component: AppConfigComponent }
];

/**
 * Routing for the app config component.
 */
export const routing = RouterModule.forChild(routes);

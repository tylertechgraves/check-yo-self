import { Routes, RouterModule, PreloadAllModules } from '@angular/router';
import { EmployeesComponent } from './employees/employees.component';

const routes: Routes = [
    { path: 'employees', component: EmployeesComponent }
];

export const employeesRouting = RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules });

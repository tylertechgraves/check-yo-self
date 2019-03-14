import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EmployeesComponent } from './employees/employees.component';
import { EmployeesService } from './employees.service';
import { employeesRouting } from './employees.routes';

@NgModule({
  imports: [
    CommonModule,
    employeesRouting
  ],
  declarations: [EmployeesComponent],
  providers: [EmployeesService]
})
export class EmployeesModule { }

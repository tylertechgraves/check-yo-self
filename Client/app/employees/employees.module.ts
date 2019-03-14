import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EmployeesComponent } from './employees/employees.component';
import { EmployeesService } from './employees.service';
import { employeesRouting } from './employees.routes';
import { FlexLayoutModule } from '@angular/flex-layout';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule, MatButtonModule, MatInputModule, MatDividerModule, MatTabsModule, MatDatepickerModule, MatNativeDateModule, MatCardModule } from '@angular/material';
import { ListEmployeesComponent } from './list-employees/list-employees.component';
import { EditEmployeesComponent } from './edit-employees/edit-employees.component';
import { AddEmployeesComponent } from './add-employees/add-employees.component';

@NgModule({
  imports: [
    CommonModule,
    employeesRouting,
    MatFormFieldModule,
    MatButtonModule,
    MatInputModule,
    MatDividerModule,
    MatTabsModule,
    MatDatepickerModule,
    MatNativeDateModule,
    FlexLayoutModule,
    FormsModule,
    ReactiveFormsModule,
    MatCardModule
  ],
  declarations: [EmployeesComponent, ListEmployeesComponent, EditEmployeesComponent, AddEmployeesComponent],
  providers: [EmployeesService]
})
export class EmployeesModule { }

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EmployeesComponent } from './employees/employees.component';
import { EmployeesService } from './employees.service';
import { employeesRouting } from './employees.routes';
import { FlexLayoutModule } from '@angular/flex-layout';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule, MatButtonModule, MatInputModule, MatDividerModule, MatTabsModule, MatDialogModule } from '@angular/material';
import { MatDatepickerModule, MatNativeDateModule, MatCardModule, MatSnackBarModule, MatRadioModule } from '@angular/material';
import { ListEmployeesComponent } from './list-employees/list-employees.component';
import { EditEmployeesComponent, EditEmployeeDialogComponent } from './edit-employees/edit-employees.component';
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
    MatCardModule,
    MatSnackBarModule,
    MatRadioModule,
    MatDialogModule
  ],
  declarations: [EmployeesComponent, ListEmployeesComponent, EditEmployeesComponent, AddEmployeesComponent, EditEmployeeDialogComponent],
  entryComponents: [EditEmployeeDialogComponent],
  providers: [EmployeesService]
})
export class EmployeesModule { }

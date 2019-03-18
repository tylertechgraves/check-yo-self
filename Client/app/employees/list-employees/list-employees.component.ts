import { Component, OnInit } from '@angular/core';
import { EmployeesService } from '../employees.service';
import { Employee } from '../../core/models/employee';
import { MatDialog } from '@angular/material';
import { EditEmployeeDialogComponent } from '../edit-employee-dialog/edit-employee-dialog.component';

@Component({
  selector: 'appc-list-employees',
  templateUrl: './list-employees.component.html',
  styleUrls: ['./list-employees.component.scss']
})
export class ListEmployeesComponent implements OnInit {
  public employeesList: Employee[];

  constructor(
    private employeesService: EmployeesService,
    public dialog: MatDialog
  ) { }

  ngOnInit() {
  }

  getEmployees() {
    this.employeesService.getEmployees().subscribe((result: Employee[]) => {
      this.employeesList = result;
    });
  }

  openEditDialog(employee: Employee) {
    const dialogRef = this.dialog.open(EditEmployeeDialogComponent, {
      width: '250px',
      height: '315px',
      data: employee
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.getEmployees();
      }
    });
  }
}

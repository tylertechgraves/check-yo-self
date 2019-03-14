import { Component, OnInit } from '@angular/core';
import { EmployeesService } from '../employees.service';
import { Employee } from '../../core/models/employee';

@Component({
  selector: 'appc-list-employees',
  templateUrl: './list-employees.component.html',
  styleUrls: ['./list-employees.component.scss']
})
export class ListEmployeesComponent implements OnInit {
  public employeesList: Employee[];

  constructor(
    private employeesService: EmployeesService
  ) { }

  ngOnInit() {
  }

  getEmployees() {
    this.employeesService.getEmployees().subscribe((result: Employee[]) => {
      this.employeesList = result;
    });
  }
}

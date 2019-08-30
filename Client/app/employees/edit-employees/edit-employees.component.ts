import { Component, OnInit, Input, ViewChild } from '@angular/core';
import { FormGroup, NgForm } from '@angular/forms';
import { EmployeesService } from '../employees.service';
import { Employee } from '../../core/models/employee';
import { MatDialog } from '@angular/material';
import { EditEmployeeDialogComponent } from '../edit-employee-dialog/edit-employee-dialog.component';

@Component({
  selector: 'appc-edit-employees',
  templateUrl: './edit-employees.component.html',
  styleUrls: ['./edit-employees.component.scss']
})
export class EditEmployeesComponent implements OnInit {
  @Input() editFormGroup: FormGroup;

  public queryOptions: string[] = ['ID', 'Full Name', 'Last Name'];
  public queryOption: string;
  public employeesList: Employee[];
  public formIsClean: boolean;
  public querying: boolean;

  @ViewChild('formDirective') private formDirective: NgForm;
  private formEmployee: Employee;

  constructor(
    private employeesService: EmployeesService,
    public dialog: MatDialog
  ) { }

  ngOnInit() {
    this.queryOption = 'ID';
    this.setUpFormModelSubscription();
    this.formIsClean = true;
    this.employeesList = [];
  }

  setUpFormModelSubscription() {
    this.editFormGroup.valueChanges.subscribe(formModel => {
      // Retrieve the form model
      this.formEmployee = this.editFormGroup.value;
    });
  }

  searchForEmployee() {
    this.querying = true;

    if (this.queryOption === 'ID') {
      this.employeesService.searchForEmloyeeById(this.formEmployee.employeeId).subscribe((response: Employee) => {
        if (response === null) {
          this.employeesList = [];
        } else {
          this.employeesList = [response];
        }

        this.querying = false;
      },
      (error: any) => {
        // If we get an error back, let's just assume no employees matched the id.
        this.employeesList = [];
        this.querying = false;
      });
    }

    if (this.queryOption === 'Full Name') {
      this.employeesService.searchForEmloyeeByFullName(this.formEmployee.lastName, this.formEmployee.firstName).subscribe((response: Employee[]) => {
        this.employeesList = response;
        this.querying = false;
      },
      (error: any) => {
        // If we get an error back, let's just assume no employees matched the id.
        this.employeesList = [];
        this.querying = false;
      });
    }

    if (this.queryOption === 'Last Name') {
      this.employeesService.searchForEmloyeeByLastName(this.formEmployee.lastName).subscribe((response: Employee[]) => {
        this.employeesList = response;
        this.querying = false;
      },
      (error: any) => {
        // If we get an error back, let's just assume no employees matched the id.
        this.employeesList = [];
        this.querying = false;
      });
    }

    this.formIsClean = false;
  }

  onRadioButtonChanged() {
    this.formDirective.resetForm();
    this.employeesList = [];
    this.formIsClean = true;
  }

  openEditDialog(employee: Employee) {
    const dialogRef = this.dialog.open(EditEmployeeDialogComponent, {
      width: '250px',
      height: '315px',
      data: employee
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        if (this.queryOption === 'ID' || this.queryOption === 'Last Name') {
          this.searchForEmployee();
        } else {
          this.employeesList = [];

          this.formIsClean = true;
          this.formDirective.resetForm();
          this.editFormGroup.reset();
        }
      }
    });
  }
}

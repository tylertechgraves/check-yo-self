import { Component, OnInit, Input, Inject } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { EmployeesService } from '../employees.service';
import { Employee } from '../../core/models/employee';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

@Component({
  selector: 'appc-edit-employees',
  templateUrl: './edit-employees.component.html',
  styleUrls: ['./edit-employees.component.scss']
})
export class EditEmployeesComponent implements OnInit {
  @Input() editFormGroup: FormGroup;

  public queryOptions: string[] = ['ID', 'Name'];
  public queryOption: string;
  public employeesList: Employee[];
  private formEmployee: Employee;
  private selectedEmployee: Employee;

  constructor(
    private employeesService: EmployeesService,
    public dialog: MatDialog
  ) { }

  ngOnInit() {
    this.queryOption = 'ID';
    this.setUpFormModelSubscription();
  }

  setUpFormModelSubscription() {
    this.editFormGroup.valueChanges.subscribe(formModel => {
      // Retrieve the form model
      this.formEmployee = this.editFormGroup.value;
    });
  }

  editEmployee() {

  }

  searchForEmployee() {
    if (this.queryOption === 'ID') {
      this.employeesService.searchForEmloyeeById(this.formEmployee.employeeId).subscribe((response: Employee) => {
        if (response === null) {
          this.employeesList = [];
        } else {
          this.employeesList = [response];
        }
      });
    } else {
      this.employeesService.searchForEmloyeeByFullName(this.formEmployee.lastName, this.formEmployee.firstName).subscribe((response: Employee[]) => {
        this.employeesList = response;
      });
    }
  }

  openEditDialog(employee: Employee) {
    this.selectedEmployee = employee;

    const dialogRef = this.dialog.open(EditEmployeeDialogComponent, {
      width: '250px',
      height: '315px',
      data: employee
    });

    dialogRef.afterClosed().subscribe(result => {
      this.selectedEmployee = result;
    });
  }
}

@Component({
  selector: 'edit-employee-dialog',
  templateUrl: 'edit-employee-dialog.html',
})
export class EditEmployeeDialogComponent implements OnInit {
  public editFormGroup: FormGroup;

  constructor(
    public dialogRef: MatDialogRef<EditEmployeeDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public employee: Employee,
    private _formBuilder: FormBuilder) {}

    ngOnInit() {
      this.createFormGroup();
      this.setUpFormModelSubscription();
    }

  createFormGroup() {
    this.editFormGroup = this._formBuilder.group({
      firstName: [this.employee.firstName, Validators.required],
      lastName: [this.employee.lastName, Validators.required],
      salary: [this.employee.salary, [Validators.min(1), Validators.required]],
      firstPaycheckDate: [this.employee.firstPaycheckDate, Validators.required]
    });
  }

  setUpFormModelSubscription() {
    this.editFormGroup.valueChanges.subscribe(formModel => {
      // Retrieve the form model
      this.employee = this.editFormGroup.value;
    });
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  editEmployee() {

  }

}

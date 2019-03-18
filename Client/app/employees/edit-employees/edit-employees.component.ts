import { Component, OnInit, Input, Inject, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators, NgForm } from '@angular/forms';
import { EmployeesService } from '../employees.service';
import { Employee } from '../../core/models/employee';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA, MatSnackBar, MatSnackBarHorizontalPosition } from '@angular/material';

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
  public formIsClean: boolean;

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
  }

  setUpFormModelSubscription() {
    this.editFormGroup.valueChanges.subscribe(formModel => {
      // Retrieve the form model
      this.formEmployee = this.editFormGroup.value;
    });
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

    this.formIsClean = false;
  }

  openEditDialog(employee: Employee) {
    const dialogRef = this.dialog.open(EditEmployeeDialogComponent, {
      width: '250px',
      height: '315px',
      data: employee
    });

    dialogRef.afterClosed().subscribe(result => {
      this.employeesList = [];

      this.formIsClean = true;
      this.formDirective.resetForm();
      this.editFormGroup.reset();
    });
  }
}

@Component({
  selector: 'edit-employee-dialog',
  templateUrl: 'edit-employee-dialog.html',
})
export class EditEmployeeDialogComponent implements OnInit {
  public editFormGroup: FormGroup;
  private horizontalPosition: MatSnackBarHorizontalPosition = 'center';

  constructor(
    @Inject(MAT_DIALOG_DATA) public employee: Employee,
    public dialogRef: MatDialogRef<EditEmployeeDialogComponent>,
    private _formBuilder: FormBuilder,
    private employeesService: EmployeesService,
    private snackBar: MatSnackBar ) {}

    ngOnInit() {
      this.createFormGroup();
      this.setUpFormModelSubscription();
    }

  createFormGroup() {
    this.editFormGroup = this._formBuilder.group({
      employeeId: [this.employee.employeeId],
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
    this.employeesService.editEmployee(this.employee).subscribe(response => {
      const responseData = response;

      this.openSnackBar('Employee has been updated in the employees database', '');

      this.dialogRef.close();
    });
  }

  private openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action, {
      duration: 2000,
      horizontalPosition: this.horizontalPosition
    });
  }
}

import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatSnackBarHorizontalPosition, MAT_DIALOG_DATA, MatDialogRef, MatSnackBar } from '@angular/material';
import { Employee } from '../../core/models/employee';
import { EmployeesService } from '../employees.service';

@Component({
  selector: 'edit-employee-dialog',
  templateUrl: 'edit-employee-dialog.component.html',
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

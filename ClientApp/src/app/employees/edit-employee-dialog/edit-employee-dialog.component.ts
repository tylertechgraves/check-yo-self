import { Component, OnInit, Inject } from '@angular/core';
import { UntypedFormGroup, UntypedFormBuilder, Validators } from '@angular/forms';
import { MatSnackBar, MatSnackBarHorizontalPosition } from '@angular/material/snack-bar';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Employee } from '../../core/models/employee';
import { EmployeesService } from '../employees.service';
import { map } from 'rxjs/operators';

@Component({
  selector: 'appc-edit-employee-dialog',
  templateUrl: 'edit-employee-dialog.component.html',
})
export class EditEmployeeDialogComponent implements OnInit {
  public editFormGroup: UntypedFormGroup;
  private horizontalPosition: MatSnackBarHorizontalPosition = 'center';

  constructor(
    @Inject(MAT_DIALOG_DATA) public employee: Employee,
    public dialogRef: MatDialogRef<EditEmployeeDialogComponent>,
    private _formBuilder: UntypedFormBuilder,
    private employeesService: EmployeesService,
    private snackBar: MatSnackBar) { }

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

  // setUpFormModelSubscription() {
  //   this.editFormGroup.valueChanges.subscribe(formModel => {
  //     // Retrieve the form model
  //     this.employee = this.editFormGroup.value;
  //   });
  // }

  setUpFormModelSubscription(): void {
    this.editFormGroup.valueChanges.pipe(
      map((formData) => {
        const {salary} = formData;
        return {...formData, salary: parseFloat(salary)} as Employee;
      })
    )
    .subscribe(employee => {
      // Retrieve the form model
      this.employee = employee;
    });
  }

  onNoClick(): void {
    this.dialogRef.close([false, this.employee]);
  }

  editEmployee() {
    this.employeesService.editEmployee(this.employee).subscribe(response => {
      this.openSnackBar('Employee has been updated in the employees database', '');

      this.dialogRef.close([true, this.employee]);
    });
  }

  private openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action, {
      duration: 2000,
      horizontalPosition: this.horizontalPosition
    });
  }
}

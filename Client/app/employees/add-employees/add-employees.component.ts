import { Component, OnInit, Input, ViewChild } from '@angular/core';
import { FormGroup, NgForm } from '@angular/forms';
import { Employee } from '../../core/models/employee';
import { EmployeesService } from '../employees.service';
import { MatSnackBarHorizontalPosition, MatSnackBar } from '@angular/material';
import { THIS_EXPR } from '@angular/compiler/src/output/output_ast';

@Component({
  selector: 'appc-add-employees',
  templateUrl: './add-employees.component.html',
  styleUrls: ['./add-employees.component.scss']
})
export class AddEmployeesComponent implements OnInit {
  @Input() addFormGroup: FormGroup;
  @ViewChild('formDirective') private formDirective: NgForm;

  private horizontalPosition: MatSnackBarHorizontalPosition = 'center';
  private employee: Employee;

  constructor(
    private employeeService: EmployeesService,
    private snackBar: MatSnackBar
  ) { }

  ngOnInit() {
    this.setUpFormModelSubscription();
  }

  setUpFormModelSubscription() {
    this.addFormGroup.valueChanges.subscribe(formModel => {
      // Retrieve the form model
      this.employee = this.addFormGroup.value;
    });
  }

  addEmployee() {
    if (this.addFormGroup.valid) {
      this.employeeService.addEmployee(this.employee).subscribe((response: Employee) => {
        this.employee = new Employee();
        this.formDirective.resetForm();
        this.addFormGroup.reset({firstPaycheckDate: new Date()});

        this.openSnackBar('Employee has been added to the employees database', '');
      });
    }
  }

  private openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action, {
      duration: 2000,
      horizontalPosition: this.horizontalPosition
    });
  }
}

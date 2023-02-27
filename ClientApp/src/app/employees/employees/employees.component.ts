import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UntypedFormGroup, UntypedFormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'appc-employees',
  templateUrl: './employees.component.html',
  styleUrls: ['./employees.component.scss']
})
export class EmployeesComponent implements OnInit {
  public employeesForm: UntypedFormGroup;

  constructor(
    private _router: Router,
    private _formBuilder: UntypedFormBuilder
  ) { }

  ngOnInit() {
    this.createForm();
  }

  createForm() {
    this.employeesForm = this._formBuilder.group({
      editParameters: this._formBuilder.group({
        employeeId: ['', [Validators.min(1), Validators.pattern('^[0-9]*$')]],
        lastName: [''],
        firstName: ['']
      }),
      addParameters: this._formBuilder.group({
        firstName: ['', Validators.required],
        lastName: ['', Validators.required],
        salary: ['', [Validators.min(1), Validators.required]],
        firstPaycheckDate: [new Date(), Validators.required]
      })
    });
  }

  goToCheckCalculator() {
    this._router.navigateByUrl('/paycheck-calculator');
  }

}

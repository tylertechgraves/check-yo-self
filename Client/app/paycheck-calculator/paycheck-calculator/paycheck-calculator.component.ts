import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Parameters } from '../models/parameters.model';
import { ConfigService } from '../../core/services/config.service';
import { Router } from '@angular/router';

@Component({
  selector: 'appc-paycheck-calculator',
  templateUrl: './paycheck-calculator.component.html',
  styleUrls: ['./paycheck-calculator.component.scss']
})
export class PaycheckCalculatorComponent implements OnInit {
  public checkCalculatorForm: FormGroup;
  public parameters: Parameters;
  public employeesButtonVisible: boolean;

  constructor(
    private _router: Router,
    private _formBuilder: FormBuilder
    ) { }

  ngOnInit() {
    // Create an empty root form group.
    // This will create a set of controls that are empty and ready to be filled in.
    this.createForm();
    this.employeesButtonVisible = ConfigService.config.checkYoSelf.employeesButtonVisible;
  }

  createForm() {
    this.checkCalculatorForm = this._formBuilder.group({
      datesParameters: this._formBuilder.group({
        startDate: [new Date(), Validators.required],
        numberOfYearsToCheck: [1, [Validators.max(10), Validators.min(1)]],
        payPeriodDays: [7, [Validators.max(31), Validators.min(1)]]
      }),
      monthsParameters: this._formBuilder.group({
        startDate: [new Date(), Validators.required],
        numberOfChecksInAGivenMonth: [1, [Validators.max(31), Validators.min(1)]],
        payPeriodDays: [7, [Validators.max(31), Validators.min(1)]]
      })
    });
  }

  setUpFormModelSubscription() {
    this.checkCalculatorForm.valueChanges.subscribe(formModel => {
      // Retrieve the form model
      const parameters = this.checkCalculatorForm.value;

      // Copy the updated form model to the petRegistration object
      this.parameters = parameters;
    });
  }

  goToEmployeesPage() {
    this._router.navigateByUrl('/employees');
  }
}

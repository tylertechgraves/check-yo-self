import { Component, OnInit, Input } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { PaycheckCalculatorService } from '../paycheck-calculator.service';
import { Paycheck } from '../models/parameters.model';

@Component({
  selector: 'appc-show-paycheck-dates',
  templateUrl: './show-paycheck-dates.component.html',
  styleUrls: ['./show-paycheck-dates.component.scss']
})
export class ShowPaycheckDatesComponent implements OnInit {
  @Input() datesFormGroup: FormGroup;

  public returnedChecks: Paycheck[];

  constructor(
    private paycheckCalculatorService: PaycheckCalculatorService
  ) { }

  ngOnInit() {
  }

  getPaychecks() {
    const startDate = this.datesFormGroup.controls['startDate'].value;
    const numberOfYearsToCheck = this.datesFormGroup.controls['numberOfYearsToCheck'].value;
    const payPeriodDays = this.datesFormGroup.controls['payPeriodDays'].value;

    this.paycheckCalculatorService.getPaychecks(startDate, numberOfYearsToCheck, payPeriodDays).subscribe((result: Paycheck[]) => {
      this.returnedChecks = result;
    });
  }
}

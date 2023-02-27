import { Component, OnInit, Input } from '@angular/core';
import { UntypedFormGroup } from '@angular/forms';
import { PaycheckCalculatorService } from '../paycheck-calculator.service';
import { Month } from '../models/parameters.model';

@Component({
  selector: 'appc-show-months',
  templateUrl: './show-months.component.html',
  styleUrls: ['./show-months.component.scss']
})
export class ShowMonthsComponent implements OnInit {
  @Input() monthsFormGroup: UntypedFormGroup;

  public returnedMonths: Month[];

  constructor(
    private paycheckCalculatorService: PaycheckCalculatorService
  ) { }

  ngOnInit() {
  }

  getPaycheckMonths() {
    const startDate = this.monthsFormGroup.controls['startDate'].value;
    const numberOfChecksInAGivenMonth = this.monthsFormGroup.controls['numberOfChecksInAGivenMonth'].value;
    const payPeriodDays = this.monthsFormGroup.controls['payPeriodDays'].value;

    this.paycheckCalculatorService.getPaycheckMonths(startDate, numberOfChecksInAGivenMonth, payPeriodDays).subscribe((result: Month[]) => {
      this.returnedMonths = result;
    });
  }

}

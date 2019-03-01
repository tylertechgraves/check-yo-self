import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PaycheckCalculatorComponent } from './paycheck-calculator/paycheck-calculator.component';
import { routing } from './paycheck-calculator.routes';
import { PaycheckCalculatorService } from './paycheck-calculator.service';
import { FlexLayoutModule } from '@angular/flex-layout';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ShowPaycheckDatesComponent } from './show-paycheck-dates/show-paycheck-dates.component';
import { ShowMonthsComponent } from './show-months/show-months.component';
import { MatFormFieldModule, MatButtonModule, MatInputModule, MatDividerModule, MatTabsModule, MatListModule, MatDatepickerModule, MatNativeDateModule } from '@angular/material';

@NgModule({
  imports: [
    CommonModule,
    FlexLayoutModule,
    FormsModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatButtonModule,
    MatInputModule,
    MatDividerModule,
    MatTabsModule,
    MatListModule,
    MatDatepickerModule,
    MatNativeDateModule,
    routing
  ],
  declarations: [PaycheckCalculatorComponent, ShowPaycheckDatesComponent, ShowMonthsComponent],
  providers: [PaycheckCalculatorService]
})
export class PaycheckCalculatorModule { }

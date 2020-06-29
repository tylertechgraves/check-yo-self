import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PaycheckCalculatorComponent } from './paycheck-calculator/paycheck-calculator.component';
import { routing } from './paycheck-calculator.routes';
import { PaycheckCalculatorService } from './paycheck-calculator.service';
import { FlexLayoutModule } from '@angular/flex-layout';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ShowPaycheckDatesComponent } from './show-paycheck-dates/show-paycheck-dates.component';
import { ShowMonthsComponent } from './show-months/show-months.component';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule} from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatDividerModule } from '@angular/material/divider';
import { MatTabsModule } from '@angular/material/tabs';
import { MatListModule } from '@angular/material/list';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';

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

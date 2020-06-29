import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS, HttpClient } from '@angular/common/http';

import 'hammerjs/hammer';

import { CoreModule } from './core/core.module';
import { SharedModule } from './shared/shared.module';
import { routing } from './app.routes';
import { AppComponent } from './app.component';
import { PaycheckCalculatorModule } from './paycheck-calculator/paycheck-calculator.module';
import { EmployeesModule } from './employees/employees.module';

@NgModule({
    declarations: [AppComponent],
    imports: [
        BrowserAnimationsModule,
        BrowserModule,
        routing,
        HttpClientModule,
        BrowserAnimationsModule,
        CoreModule.forRoot(),
        SharedModule,
        PaycheckCalculatorModule,
        EmployeesModule
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }

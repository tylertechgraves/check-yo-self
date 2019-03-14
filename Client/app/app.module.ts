import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS, HttpClient } from '@angular/common/http';

import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { TranslatePoHttpLoader } from '@biesbjerg/ngx-translate-po-http-loader';
import 'hammerjs/hammer';

import { CoreModule } from './core/core.module';
import { SharedModule } from './shared/shared.module';
import { routing } from './app.routes';
import { AppComponent } from './app.component';
import { PaycheckCalculatorModule } from './paycheck-calculator/paycheck-calculator.module';
import { EmployeesModule } from './employees/employees.module';

export function createTranslateLoader(http: HttpClient) {
    return new TranslatePoHttpLoader(http, 'i18n', '.po');
}

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
        TranslateModule.forRoot({
            loader: {
                provide: TranslateLoader,
                useFactory: createTranslateLoader,
                deps: [HttpClient]
            }
        }),
        PaycheckCalculatorModule,
        EmployeesModule
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }

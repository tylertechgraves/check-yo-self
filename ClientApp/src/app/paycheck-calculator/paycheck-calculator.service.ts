import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Month, Paycheck } from './models/parameters.model';
import { ConfigService } from '../core/services/config.service';

@Injectable()
export class PaycheckCalculatorService {

  constructor(
    private httpClient: HttpClient
  ) { }

  public getPaychecks(startDate: Date, numberOfYearsToCheck: number, payPeriodDays: number): Observable<Paycheck[]> {
    const parameters = new HttpParams({
      fromObject: {
        startDate: startDate.toLocaleDateString(),
        numberOfYearsToCheck: numberOfYearsToCheck.toString(),
        payPeriodDays: payPeriodDays.toString()
      }
    });

    const checksEndpoint: string = ConfigService.config.checkYoSelf.checksApiEndpoint;

    return this.httpClient.get<Paycheck[]>(checksEndpoint, { params: parameters });
  }

  public getPaycheckMonths(startDate: Date, numberOfChecksInAGivenMonth: number, payPeriodDays: number): Observable<Month[]> {
    const parameters = new HttpParams({
      fromObject: {
        startDate: startDate.toLocaleDateString(),
        numberOfChecksInAGivenMonth: numberOfChecksInAGivenMonth.toString(),
        payPeriodDays: payPeriodDays.toString()
      }
    });

    const monthsEndpoint: string = ConfigService.config.checkYoSelf.monthsApiEndpoint;

    return this.httpClient.get<Month[]>(monthsEndpoint, { params: parameters });
  }
}

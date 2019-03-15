import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { Employee } from '../core/models/employee';
import { HttpClient } from '@angular/common/http';
import { ConfigService } from '../core/services/config.service';
import { Config } from '../core/models/config';

@Injectable()
export class EmployeesService {

  constructor(
    private httpClient: HttpClient
  ) { }

  public getEmployees(): Observable<Employee[]> {
    const listEmployeesEndpoint: string = ConfigService.config.checkYoSelf.listEmployeesEndpoint;

    return this.httpClient.get<Employee[]>(listEmployeesEndpoint);
  }

  public addEmployee(employee: Employee): Observable<Employee> {
    const addEmployeeEndpoint = ConfigService.config.checkYoSelf.listEmployeesEndpoint;

    return this.httpClient.post<Employee>(addEmployeeEndpoint, employee);
  }

}

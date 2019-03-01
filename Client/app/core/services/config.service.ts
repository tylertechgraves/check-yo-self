import { Injectable } from '@angular/core';
import { Http } from '@angular/http';

import { Config } from '../models/config';

/**
 * Angular service to fetch application configuration
 */
@Injectable()
export class ConfigService {
  /**
   * Private value to store application configuration
   */
  private static _config: Config;

  /**
   * Public accessor to get the application configuration in other components
   */
  public static get config() {
    return ConfigService._config;
  }

  /**
   * Create an instance of {@link ConfigService}
   * @param {Http} http
   */
  constructor(private http: Http) { }

  /**
   * Method used during bootstrap to fetch the application configuration
   */
  load(): Promise<any> {
    const promise: Promise<any> = new Promise((resolve: any) => {
      this.http.get('api/AppConfig/')
        .map(res => res.json())
        .subscribe(res => {
          ConfigService._config = <any>res;
          setTimeout(() => resolve(true), 1);
        }, err => {
          console.log(err);
        });
    });
    return promise;
  }
}

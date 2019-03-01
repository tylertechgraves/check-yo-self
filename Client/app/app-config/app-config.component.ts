import { Component, OnInit } from '@angular/core';

import { ConfigService } from '../core/services/config.service';
import { Config } from '../core/models/config';

/**
 * Component to display currently running configuration
 */
@Component({
  selector: 'appc-app-config',
  templateUrl: './app-config.component.html',
  styleUrls: ['./app-config.component.scss']
})
export class AppConfigComponent implements OnInit {

  /**
   * Configuration object used for databinding in the ui
   */
  public config: Config;

  /**
   * Create a new instance of {@link AppConfigComponent}
   */
  constructor() { }

  /**
   * Angular OnInit lifecycle hook
   */
  ngOnInit() {
    this.config = ConfigService.config;
  }

  /**
   * Method that gets all properties from an object and returns them as an array of key/value pairs
   * @param {Object} o
   */
  getProperties(o: Object) {
    const result: any[] = [];
    for (const prop in o) {
      if (o.hasOwnProperty(prop)) {
        const val: any = (<any>o)[prop];
        if (typeof val !== 'object') {
          result.push({ propertyName: prop, propertyValue: val });
        }
      }
    }
    return result;
  }
}

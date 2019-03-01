import { Component, OnInit, ViewEncapsulation, OnDestroy } from '@angular/core';
import { ConfigService } from './core/services/config.service';
import { AppInsightsConfig, AppInsightsService } from '@markpieszak/ng-application-insights';
import { Subscription } from 'rxjs/Subscription';
import { TranslateService } from '@ngx-translate/core';
import 'rxjs/add/operator/filter';

/**
 * Application entry point
 */
@Component({
  selector: 'appc-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class AppComponent implements OnInit, OnDestroy {


  /**
   * Create an instance of {@link AppComponent}
   */
  constructor(
    translateService: TranslateService,
    appInsightsService: AppInsightsService
  ) {
    translateService.setDefaultLang(ConfigService.config.defaultLanguage);
    translateService.addLangs(['en', 'es']);
    translateService.use(translateService.getBrowserLang());

    console.log(`${translateService.getBrowserLang()} :: ${translateService.getBrowserCultureLang()}`);

    appInsightsService.config = new AppInsightsConfig();
    const configSettings = ConfigService.config.applicationInsights;
    appInsightsService.config.instrumentationKey = configSettings.instrumentationKey;
    appInsightsService.config.disableTelemetry = !configSettings.telemetryEnabled;
    appInsightsService.init();
  }

  /**
   * Angular ngOnInit function
   */
  public ngOnInit() {
    const vm = this;
  }

  /**
   * Angular ngOnDestroy function
   */
  ngOnDestroy() {
  }
}

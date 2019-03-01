import { NgModule, Optional, SkipSelf, ModuleWithProviders, APP_INITIALIZER } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Title } from '@angular/platform-browser';
import { AppInsightsService } from '@markpieszak/ng-application-insights';

// Services
import { ConfigService } from './services/config.service';
import { DataService } from './services/data.service';
import { UtilityService } from './services/utility.service';

export function loadConfigService(configService: ConfigService): () => Promise<any> {
    return () => configService.load();
}

@NgModule({
    imports: [CommonModule],
    exports: [],
    providers: [AppInsightsService]
})
export class CoreModule {
    public static forRoot(): ModuleWithProviders {
        return {
            ngModule: CoreModule,
            providers: [
                Title,
                UtilityService,
                DataService,
                ConfigService,
                {
                    provide: APP_INITIALIZER,
                    useFactory: loadConfigService,
                    deps: [ConfigService],
                    multi: true
                },
            ]
        };
    }
    constructor( @Optional() @SkipSelf() parentModule: CoreModule) {
        if (parentModule) {
            throw new Error('CoreModule is already loaded. Import it in the AppModule only');
        }
    }
}

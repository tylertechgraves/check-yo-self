import './polyfills';

import 'rxjs/add/operator/map';
import 'rxjs/add/operator/first';
import 'rxjs/add/operator/mapTo';
import 'rxjs/add/operator/scan';
import 'rxjs/add/operator/bufferCount';
import 'rxjs/add/operator/debounceTime';
import 'rxjs/add/operator/startWith';
import 'rxjs/add/operator/finally';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/interval';
import 'rxjs/add/observable/throw';
import 'rxjs/add/observable/merge';

// declare const module: any;

import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { AppModule } from './app/app.module';

/**
 * Get the dist path relative to the application.
 */
const distPath: string = document.getElementsByTagName('base')[0].href + 'dist/';

/**
 * Magic variable used by the webpack file-loader.
 */
declare let __webpack_public_path__: string;
__webpack_public_path__ = distPath;

/**
 * This is the internal variable that stores the return module.
 */
let modulePromise: Promise<any>;

// Enable either Hot Module Reloading or production mode
/* tslint:disable */
if ((<any>module)['hot']) {
    (<any>module)['hot'].accept();
    (<any>module)['hot'].dispose(() => { 
        modulePromise.then(appModule => appModule.destroy());
    });
} else {
    enableProdMode();
}

modulePromise = platformBrowserDynamic().bootstrapModule(AppModule);

// Generated on 02/18/2019 with Dapper Diablo 1.0.58
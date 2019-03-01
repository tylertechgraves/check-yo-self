import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { Subject } from 'rxjs/Subject';

import { UtilityService } from './utility.service';
import { DataServiceOptions } from './data-service-options';

/**
 * DataService is a specialized HTTP client
 */
@Injectable()
export class DataService {

    /**
     * Internal Subject used to push the command count
     */
    public pendingCommandsSubject = new Subject<number>();

    /**
     * Pending command count.  Incremented when a request starts and decremented after it's result is handled
     */
    public pendingCommandCount = 0;

    /**
     * Public Observable that clients can subscribe to
     */
    public pendingCommands$: Observable<number>;

    /**
     * Create an instance of {@link DataService}
     * @param {Http} http Angular Http Service
     * @param {UtilityService} us Utility Service
     */
    constructor(public http: HttpClient, public us: UtilityService) {
        this.pendingCommands$ = this.pendingCommandsSubject.asObservable();
    }

    /**
     * GET requests to the API, appending the given params as URL search parameters.
     * @param {string} url
     * @param {any} params
     * @returns Stream
     */
    public get(url: string, params?: any): Observable<Response> {
        const options = new DataServiceOptions();
        options.method = 'GET';
        options.url = url;
        options.params = params;
        return this.request(options);
    }

    /**
     * POST requests to the API.
     *
     * If both the params and data are present, the params will be appended
     * as URL search parameters and the data will be serialized as a JSON
     * payload. If only the data is present, it will be serialized as a JSON
     * payload.
     * @param {string} url
     * @param {any} data
     * @param {any} params
     * @returns Stream
     */
    public post(url: string, data?: any, params?: any): Observable<Response> {
        if (!data) {
            data = params;
            params = {};
        }
        const options = new DataServiceOptions();
        options.method = 'POST';
        options.url = url;
        options.params = params;
        options.data = data;
        return this.request(options);
    }

    /**
     * PUT requests to the API.
     *
     * If both the params and data are present, the params will be appended
     * as URL search parameters and the data will be serialized as a JSON
     * payload. If only the data is present, it will be serialized as a JSON
     * payload.
     * @param {string} url
     * @param {any} data
     * @param {any} params
     */
    public put(url: string, data?: any, params?: any): Observable<Response> {
        if (!data) {
            data = params;
            params = {};
        }
        const options = new DataServiceOptions();
        options.method = 'PUT';
        options.url = url;
        options.params = params;
        options.data = data;
        return this.request(options);
    }

    /**
     * DELETE requests to the API.
     * @param {string} url
     */
    public delete(url: string): Observable<Response> {
        const options = new DataServiceOptions();
        options.method = 'DELETE';
        options.url = url;
        return this.request(options);
    }

    /**
     * Internal method used by get, put, post and delete methods
     * @param {DataServiceOptions} options
     */
    private request(options: DataServiceOptions): Observable<any> {
        options.method = (options.method || 'GET');
        options.url = (options.url || '');
        options.headers = (options.headers || {});
        options.params = (options.params || {});
        options.data = (options.data || {});

        this.interpolateUrl(options);
        this.addXsrfToken(options);
        this.addContentType(options);

        const requestOptions = {
            headers: options.headers,
            params: options.params,
            body: JSON.stringify(options.data)
        }

        this.pendingCommandsSubject.next(++this.pendingCommandCount);

        const stream = this.http.request(options.method.toString(), options.url, requestOptions)
            .catch((error: any) => { return Observable.throw(error); })
            .finally(() => { this.pendingCommandsSubject.next(--this.pendingCommandCount); });

        return stream;
    }

    /**
     * Internal method that sets content-type to application/json
     * @param {DataServiceOptions} options
     */
    private addContentType(options: DataServiceOptions): DataServiceOptions {
        // if (options.method !== RequestMethod.Get) {
        options.headers['Content-Type'] = 'application/json; charset=UTF-8';
        // }
        return options;
    }

    /**
     * Internal method to extract (remove and return) a value from an array
     * @param {any} collection
     * @param {string} key
     */
    private extractValue(collection: any, key: string): any {
        const value = collection[key];
        delete (collection[key]);
        return value;
    }

    /**
     * Internal method to add the X-XSRF-TOKEN header to a request
     * @param {DataServiceOptions} options
     */
    private addXsrfToken(options: DataServiceOptions): DataServiceOptions {
        const xsrfToken = this.getXsrfCookie();
        if (xsrfToken) {
            options.headers['X-XSRF-TOKEN'] = xsrfToken;
        }
        return options;
    }

    /**
     * Internal method to get the XSRF-TOKEN from a cookie
     */
    private getXsrfCookie(): string {
        const matches = document.cookie.match(/\bXSRF-TOKEN=([^\s;]+)/);
        try {
            return matches ? decodeURIComponent(matches[1]) : '';
        } catch (decodeError) {
            return '';
        }
    }

    /**
     * Internal method to normalize a url
     * @param {DataServiceOptions} options
     */
    private interpolateUrl(options: DataServiceOptions): DataServiceOptions {
        options.url = options.url.replace(/:([a-zA-Z]+[\w-]*)/g, ($0, token) => {
            // Try to move matching token from the params collection.
            if (options.params.hasOwnProperty(token)) {
                return (this.extractValue(options.params, token));
            }
            // Try to move matching token from the data collection.
            if (options.data.hasOwnProperty(token)) {
                return (this.extractValue(options.data, token));
            }
            // If a matching value couldn't be found, just replace
            // the token with the empty string.
            return ('');
        });
        // Clean up any repeating slashes.
        if (options.url.indexOf('://') > -1) {
            const [protocol, url] = options.url.split('://');
            const strippedUrl = url.replace(/\/{2,}/g, '/');
            options.url = `${protocol}://${strippedUrl}`;
        } else {
            options.url = options.url.replace(/\/{2,}/g, '/');
        }
        // Clean up any trailing slashes.
        options.url = options.url.replace(/\/+$/g, '');

        return options;
    }
}

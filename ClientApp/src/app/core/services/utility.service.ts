import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

/**
 * Helper methods
 */
@Injectable()
export class UtilityService {
    /**
     * Internal router property
     */
    private _router: Router;

    /**
     * Create an instance of {@link UtilityService}
     * @param {Router} router
     */
    constructor(router: Router) {
        this._router = router;
    }

    /**
     * Format date time
     * @param {Date} date
     */
    public convertDateTime(date: Date): string {
        const _formattedDate = new Date(date.toString());
        return _formattedDate.toDateString();
    }

    /**
     * Navigate using the angular router
     * @param {string} path
     */
    public navigate(path: string) {
        this._router.navigate([path]);
    }

    /**
     * Read query parameters from url
     */
    public getParams() {
        const searchParams = window.location.search.split('?')[1];
        if (searchParams) {
            const paramsObj: any = {};

            searchParams.split('&').forEach(i => {
                paramsObj[i.split('=')[0]] = i.split('=')[1];
            });
            return paramsObj;
        }
        return undefined;
    }
}

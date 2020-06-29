import { Component } from '@angular/core';

/**
 * Footer component
 */
@Component({
    selector: 'appc-footer',
    styleUrls: ['./footer.component.scss'],
    templateUrl: './footer.component.html'
})
export class FooterComponent {
    /**
     * Create an instance of {@link FooterComponent}
     * @param {AppConfigService} appConfig Application configuration service
     */
    public constructor() { }
}

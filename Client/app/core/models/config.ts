import { ApplicationInsightsConfig } from './application-insights-config';
import { ConfigurationServerConfig } from './config-server-config';
import { CheckYoSelfConfig } from './check-yo-self-config';

/**
 * Model object used for application configuration
 */
export class Config {
    /**
     * Application Title
     */
    public title: string;
    /**
     * Default Language
     */
    public defaultLanguage: string;
    /**
     * Uri where the application is running
     */
    public baseUri: string;
    /**
     * Uri where the Backend for Frontend service is running
     */
    public baseUriService: string;
    /**
     * Application Version
     */
    public version: string;
    /**
     * {@link ApplicationInsightsConfig} object
     */
    public applicationInsights: ApplicationInsightsConfig;
    /**
     * {@link ConfigurationServerConfig} object
     */
    public configurationServer: ConfigurationServerConfig;

    public checkYoSelf: CheckYoSelfConfig;
}

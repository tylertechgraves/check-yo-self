/**
 * Model object for Azure Application Insights configuration
 */
export class ApplicationInsightsConfig {
    /**
     * Used during application start up to determine if telemetry should be turned on
     */
    public telemetryEnabled: boolean;
    /**
     * Applications Insights instrumentation key
     */
    public instrumentationKey: string;
}

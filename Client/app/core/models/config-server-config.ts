/**
 * Model for Consul configuration server configuration
 */
export class ConfigurationServerConfig {
    /**
     * Base Uri of the Consul configuration server
     */
    public uri: string;
    /**
     * Number of times to retry the Consul connection before giving up
     */
    public retryCount: number;
    /**
     * Seconds to wait between Consul retry attempts
     */
    public retryIntervalSec: number;
}

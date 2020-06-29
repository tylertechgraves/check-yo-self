/**
 * Options object used by {@link DataService}
 */
export class DataServiceOptions {
    /**
     * HTTP method with which to execute a request
     */
    public method: string;

    /**
     * Url with which to perform a request
     */
    public url: string;

    /**
     * Headers to add to a request
     */
    public headers: any = {};

    /**
     * Search parameters to be included in a request
     */
    public params = {};

    /**
     * JSON object that will be stringified and passed as the request body
     */
    public data = {};
}

class LoadingInterceptor {
    constructor($rootScope/*, $log*/) {
        "ngInject";
        Object.assign(this,
        {
            request(config) {
                //console.log(this);
                //$log.log(config);
                $rootScope.$emit("page_loading", true);
                // return the same config
                return config;
            }
        });
        Object.assign(this,
        {
            response(response) {
                //console.log(this);
                //$log.log(response);
                $rootScope.$emit("page_loading", false);
                // return the same response
                return response;
            }
        });
    }
    
    
}

export default LoadingInterceptor;
"use strict";
import LoadingInterceptor from "./setup.interceptor";

function setupConfig($logProvider, $urlRouterProvider, $httpProvider) {
    "ngInject";

    // Enable log
    $logProvider.debugEnabled(true);
    $urlRouterProvider.otherwise("/products/list");
    $httpProvider.interceptors.push(LoadingInterceptor.name);

}

export default setupConfig;
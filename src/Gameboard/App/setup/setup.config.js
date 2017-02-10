"use strict";

function setupConfig($logProvider, $urlRouterProvider) {
    "ngInject";

    // Enable log
    $logProvider.debugEnabled(true);
    $urlRouterProvider.otherwise("/products/list");

}

export default setupConfig;
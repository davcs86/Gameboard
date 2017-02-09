"use strict";

function setupConfig($logProvider, $urlRouterProvider) {
    "ngInject";

    // Enable log
    $logProvider.debugEnabled(true);
    $urlRouterProvider.otherwise("/companies/list");

 }

export default setupConfig;

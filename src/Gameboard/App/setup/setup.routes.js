'use strict';

function routeConfig($urlRouterProvider) {
    'ngInject';

    $urlRouterProvider.otherwise('/');

}

export default angular
.module('setup.routes', [])
  .config(routeConfig);

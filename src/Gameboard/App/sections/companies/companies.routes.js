"use strict";

import IndexTpl from "./companies.html";

import ListTpl from "./list/list.html";
import ListCtrl from "./list/list.controller";

import EditTpl from "./edit/edit.html";
import EditCtrl from "./edit/edit.controller";

import NewTpl from "./new/new.html";
import NewCtrl from "./new/new.controller";

function routeConfig($stateProvider) {
    "ngInject";
    $stateProvider
        .state("companies",
        {
            url: "/companies",
            abstract: true,
            template: IndexTpl
        });
    $stateProvider
        .state("companies.list",
        {
            url: "/list",
            template: ListTpl,
            controller: ListCtrl.name,
            controllerAs: "companies"
        });
    $stateProvider
        .state("companies.new",
        {
            url: "/new",
            template: NewTpl,
            controller: NewCtrl.name,
            controllerAs: "companies"
        });
    $stateProvider
        .state("companies.edit",
        {
            url: "/edit/:id",
            template: EditTpl,
            controller: EditCtrl.name,
            controllerAs: "companies"
        });

}

export default routeConfig;
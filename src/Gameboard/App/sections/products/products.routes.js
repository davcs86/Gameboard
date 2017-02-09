"use strict";

import IndexTpl from "./products.html";

import ListTpl from "./list/list.html";
import ListCtrl from "./list/list.controller";

import EditTpl from "./edit/edit.html";
import EditCtrl from "./edit/edit.controller";

import NewTpl from "./new/new.html";
import NewCtrl from "./new/new.controller";

function routeConfig($stateProvider) {
    "ngInject";
    $stateProvider
        .state("products", {
            url: "/products",
            abstract: true,
            template: IndexTpl
        });
    $stateProvider
        .state("products.list", {
            url: "/list",
            template: ListTpl,
            controller: ListCtrl,
            controllerAs: "products"
        });
    $stateProvider
        .state("products.new", {
            url: "/new",
            template: NewTpl,
            controller: NewCtrl,
            controllerAs: "products"
        });
    $stateProvider
        .state("products.edit", {
            url: "/edit/:id",
            template: EditTpl,
            controller: EditCtrl,
            controllerAs: "products"
        });

}

export default routeConfig;
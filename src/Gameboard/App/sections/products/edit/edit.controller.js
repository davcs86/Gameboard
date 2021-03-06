﻿"use strict";

import {get as _get, isNull} from "lodash";
import { toNumber } from "lodash";

class ProductsEditController {
    constructor($state, $stateParams, $scope, $rootScope, ProductsService, CompaniesService, SweetAlert) {
        "ngInject";
        $rootScope.$emit("title_updated",
        {
            pageTitle: "Products",
            sectionTitle: "Edit product"
        });

        this.SweetAlert = SweetAlert;
        this.$rootScope = $rootScope;
        this.$apiService = ProductsService;
        this.$apiCompaniesService = CompaniesService;
        this.$scope = $scope;
        this.$state = $state;
        $scope.id = $stateParams.id;
        this.createForm();
        this.loadData();
    }
    createForm() {
        var vm = this;
        vm.$scope.companies = {};
        vm.$scope.schema = {
            "type": "object",
            "properties": {
                "name": {
                    "title": "Name",
                    "type": "string",
                    "minLength": 5,
                    "maxLength": 50,
                    "validationMessage": "Name can have between 5 and 50 characters, inclusive."
                },
                "price": {
                    "title": "Price",
                    "type": "number"
                },
                "companyId": {
                    "title": "Company",
                    "type": "string"
                },
                "ageRestriction": {
                    "title": "Maximum age",
                    "type": "number"
                },
                "description": {
                    "title": "Description",
                    "type": "string",
                    "maxLength": 500
                }
            },
            "required": [
                "name",
                "price",
                "companyId"
            ]
        };

        vm.$scope.form = [
            {
                "key": "name"
            }, {
                "key": "price",
                validationMessage: {
                    'wrongPrice': "Price must be between $ 1.00 and $ 1000.00, inclusive."
                },
                onChange: vm.$scope.validatePrice
            }, {
                "key": "companyId",
                type: "select",
                titleMap: []
            }, {
                "key": "ageRestriction",
                validationMessage: {
                    'wrongAge': "Maximum age must be between 0 and 100, inclusive."
                },
                onChange: vm.$scope.validateAge
            }, {
                "key": "description",
                "type": "textarea"
            }
        ];
        vm.$scope.refreshCompanies = () => {
            vm.$apiCompaniesService.readAll()
                .then((data) => {
                    vm.$scope.companies = data.map((d) => {
                        return {
                            "value": d.id,
                            "name": d.name
                        };
                    });
                    vm.$rootScope.$emit("companieslist_updated");
                },
                (error) => {
                    vm.SweetAlert.alert(`Error code: ${error}`, { title: "Error!" });
                    vm.$scope.companies = [];
                    vm.$rootScope.$emit("companieslist_updated");
                });
        };
        vm.$rootScope.$on("companieslist_updated",
        () => {
            vm.$scope.form[2].titleMap = vm.$scope.companies;
        });
        vm.$scope.refreshCompanies();

        vm.$scope.validatePrice = () => {
            const price = toNumber(vm.$scope.model.price) || 0;
            vm.$scope.$broadcast("schemaForm.error.price", "wrongPrice", price >= 1.0 && price <= 1000.0);
        };

        vm.$scope.validateAge = () => {
            const age = toNumber(vm.$scope.model.ageRestriction) || 0;
            vm.$scope.$broadcast("schemaForm.error.ageRestriction", "wrongAge", age >= 0.0 && age <= 100.0);
        };

        vm.$scope.model = {};

        vm.$scope.onSubmit = (form) => {
            vm.$scope.validatePrice();
            vm.$scope.validateAge();
            // First we broadcast an event so all fields validate themselves
            vm.$scope.$broadcast("schemaFormValidate");
            // Then we check if the form is valid
            if (form.$valid) {
                vm.$apiService.update(vm.$scope.id, vm.$scope.model)
                    .then(() => {
                        vm.SweetAlert.success("Product updated!", { title: "" });
                        vm.$rootScope.$emit("products_updated");
                    },
                    (msg) => {
                        vm.SweetAlert.alert(`Error code: ${msg}`, { title: "Error!" });
                    });
            }
        };
    }
    loadData() {
        this.$apiService.readOne(this.$scope.id)
            .then((data) => {
                // fill the model
                var k = [
                    ["id", ""], ["name", ""], ["creationTime", new Date()], ["lastModified", new Date()],
                    ["company", { name: "" }], ["price", 0], ["ageRestriction", ""], ["companyId", ""],
                    ["description", ""]
                ];
                k.forEach((l) => {
                    var v = _get(data, l[0], l[1]); // get the value, or set the default
                    this.$scope.model[l[0]] = isNull(v) ? l[1] : v;
                });
                this.$scope.$broadcast("schemaFormRedraw");
            },
            (msg) => {
                this.SweetAlert.alert(`Error code: ${msg}`, { title: "Error!" });
                this.$state.go("products.list");
            });
    }
}

export default ProductsEditController;
"use strict";

import {toSafeInteger} from "lodash";

class ProductsNewController {
    constructor($state, $scope, uiGridConstants, $rootScope, ProductsService, SweetAlert) {
        "ngInject";
        $rootScope.$emit("title_updated",
        {
            pageTitle: "Products",
            sectionTitle: "New product"
        });

        this.SweetAlert = SweetAlert;
        this.$rootScope = $rootScope;
        this.$apiService = ProductsService;
        this.$scope = $scope;
        this.$state = $state;
        this.createForm();
    }
    createForm() {
        var vm = this;
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
                    "type": "string",
                    "x-schema-form": {
                        "type": "number"
                    },
                    "validationMessage": "Price is required."
                }
            },
            "required": [
                "name",
                "price"
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
            }
        ];

        vm.$scope.validatePrice = function () {
            var price = toNumber(vm.$scope.model.price);
            vm.$scope.$broadcast("schemaForm.error.price", "wrongPrice", price >= 1.0 && price <= 1000.0);
        }

        vm.$scope.model = {
        };

        vm.$scope.onSubmit = function(form) {
            // First we broadcast an event so all fields validate themselves
            vm.$scope.$broadcast("schemaFormValidate");
            // Then we check if the form is valid
            if (form.$valid) {
                vm.$apiService.create(vm.$scope.model)
                    .then(() => {
                        vm.SweetAlert.success("Product created!", { title: "" });
                        vm.$rootScope.$emit("products_updated");
                        vm.$state.go("products.list");
                    },
                    (msg) => {
                        vm.SweetAlert.alert("Error code: " + msg, { title: "Error!" });
                    });
            }
        };
    }
}

export default ProductsNewController;
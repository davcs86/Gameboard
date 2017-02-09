"use strict";

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
                "Name": {
                    "title": "Name",
                    "type": "string",
                    "minLength": 5,
                    "maxLength": 50,
                    "validationMessage": "Name can have between 5 and 50 characters, inclusive."
                }
            },
            "required": [
                "Name"
            ]
        };

        vm.$scope.form = [
            {
                "key": "Name"
            }
        ];

        vm.$scope.model = {
            Name: ""
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
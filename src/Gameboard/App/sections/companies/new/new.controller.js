"use strict";

class CompaniesNewController {
    constructor($state, $scope, uiGridConstants, $rootScope, CompaniesService, SweetAlert) {
        "ngInject";
        $rootScope.$emit("title_updated",
        {
            pageTitle: "Companies",
            sectionTitle: "New company"
        });

        this.SweetAlert = SweetAlert;
        this.$rootScope = $rootScope;
        this.$apiService = CompaniesService;
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
                        vm.SweetAlert.success("Company created!", { title: "" });
                        vm.$rootScope.$emit("companies_updated");
                        vm.$state.go("companies.list");
                    },
                    (msg) => {
                        vm.SweetAlert.alert(`Error code: ${msg}`, { title: "Error!" });
                    });
            }
        };
    }
}

export default CompaniesNewController;
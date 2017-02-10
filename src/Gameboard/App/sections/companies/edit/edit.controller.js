"use strict";

class CompaniesEditController {
    constructor($state, $stateParams, $scope, uiGridConstants, $rootScope, CompaniesService, SweetAlert) {
        "ngInject";
        $rootScope.$emit("title_updated",
        {
            pageTitle: "Companies",
            sectionTitle: "Edit company"
        });

        this.SweetAlert = SweetAlert;
        this.$rootScope = $rootScope;
        this.$apiService = CompaniesService;
        this.$scope = $scope;
        this.$state = $state;
        $scope.id = $stateParams.id;
        this.createForm();
        this.loadData();
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
                }
            },
            "required": [
                "name"
            ]
        };

        vm.$scope.form = [
            {
                "key": "name"
            }
        ];

        vm.$scope.model = {
        
        };

        vm.$scope.onSubmit = function(form) {
            // First we broadcast an event so all fields validate themselves
            vm.$scope.$broadcast("schemaFormValidate");
            // Then we check if the form is valid
            if (form.$valid) {
                vm.$apiService.update(vm.$scope.id, vm.$scope.model)
                    .then(() => {
                        vm.SweetAlert.success("Company updated!", { title: "" });
                        vm.$rootScope.$emit("companies_updated");
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
                var k = ["id", "name", "creationTime", "lastModified"];
                k.forEach((l) => {
                    this.$scope.model[l] = data[l];
                });
                this.$scope.$broadcast("schemaFormRedraw");
            },
            (msg) => {
                this.SweetAlert.alert(`Error code: ${msg}`, { title: "Error!" });
                this.$state.go("companies.list");
            });
    }
}

export default CompaniesEditController;
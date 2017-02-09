"use strict";

class CompaniesListController {
    constructor($state, $scope, uiGridConstants, $rootScope, CompaniesService, SweetAlert) {
        "ngInject";
        var vm = this;
        this.$apiService = CompaniesService;
        this.$scope = $scope;
        $rootScope.$emit("title_updated",
        {
            pageTitle: "Companies",
            sectionTitle: "Summary"
        });

        $scope.deleteItem = function(row) {
            SweetAlert.confirm("Your will not be able to recover this company!",
                {
                    title: "Are you sure?",
                    //type: "warning",
                    showCancelButton: true,
                    confirmButtonColor: "#DD6B55",
                    confirmButtonText: "Yes, delete it!",
                    closeOnConfirm: false
                })
                .then(function(result) {
                    if (result) {
                        vm.$apiService.delete(row.entity.id)
                            .then(function() {
                                    SweetAlert.success("Company deleted!", { title: "" });
                                    $rootScope.$emit("companies_updated");
                                },
                                function(msg) {
                                    SweetAlert.alert(msg, { title: "Error!" });
                                });
                    }
                });
        };

        $scope.editItem = function(row) {
            $state.go("companies.edit", { id: row.entity.id });
        };

        $scope.gridOptions = {
            paginationPageSizes: [25, 50, 75],
            paginationPageSize: 25,
            enableSorting: true,
            enableFiltering: true
        };

        $scope.gridOptions.columnDefs = [
            {
                enableColumnMenu: false,
                enableFiltering: false,
                enableSorting: false,
                name: "Actions",
                width: 80,
                cellTemplate:
                    '<div class="ui-grid-cell-contents text-center"><a class="btn btn-xs btn-primary" title="Delete" ng-click="grid.appScope.deleteItem(row)"><i class="glyphicon glyphicon-remove"></i></a>&nbsp;&nbsp;<a class="btn btn-xs btn-primary" title="Edit" ng-click="grid.appScope.editItem(row)"><i class="glyphicon glyphicon-edit"></i></a></div>'
            },
            { name: "name", width: 300, sort: { direction: uiGridConstants.ASC }, enableHiding: true },
            { name: "creationTime", width: 200, enableHiding: true, enableFiltering: false },
            { name: "lastModified", width: 200, enableHiding: true, enableFiltering: false }
        ];

        $rootScope.$on("companies_updated",
            function() {
                vm.loadData();
            });

        this.loadData();
    }
    loadData() {
        this.$apiService.readAll()
            .then((data) => {
                this.$scope.gridOptions.data = data;
            },
            (msg) => {
                this.SweetAlert.alert(`Error code: ${msg}`, { title: "Error!" });
            });
    }
}

export default CompaniesListController;
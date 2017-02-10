"use strict";

import moment from "moment";
import numeral from "numeral";
import {get as _get, isNull} from "lodash";

class ProductsListController {
    constructor($state, $scope, uiGridConstants, $rootScope, ProductsService, SweetAlert) {
        "ngInject";
        var vm = this;
        this.$apiService = ProductsService;
        this.$scope = $scope;
        $rootScope.$emit("title_updated",
        {
            pageTitle: "Products",
            sectionTitle: "Summary"
        });

        $scope.deleteItem = (row) => {
            SweetAlert.confirm("Your will not be able to recover this product!",
                {
                    title: "Are you sure?",
                    //type: "warning",
                    showCancelButton: true,
                    confirmButtonColor: "#DD6B55",
                    confirmButtonText: "Yes, delete it!",
                    closeOnConfirm: false
                })
                .then((result) => {
                    if (result) {
                        vm.$apiService.delete(row.entity.id)
                            .then(() => {
                                    SweetAlert.success("Product deleted!", { title: "" });
                                    $rootScope.$emit("products_updated");
                                },
                                (msg) => {
                                    SweetAlert.alert(msg, { title: "Error!" });
                                });
                    }
                });
        };

        $scope.editItem = (row) => {
            $state.go("products.edit", { id: row.entity.id });
        };

        $scope.gridOptions = {
            paginationPageSizes: [12, 24, 36],
            paginationPageSize: 12,
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
            { name: "ageRestriction", width: 200, enableHiding: true, enableFiltering: false },
            { name: "price_", width: 200, enableHiding: true, enableFiltering: false },
            { name: "company_", width: 250, enableHiding: true, enableFiltering: true },
            { name: "creationTime_", width: 200, enableHiding: true, enableFiltering: false },
            { name: "lastModified_", width: 200, enableHiding: true, enableFiltering: false }
        ];

        $rootScope.$on("products_updated",
        () => {
            this.loadData();
        });

        this.loadData();
    }
    loadData() {
        console.log("loadData");
        this.$apiService.readAll()
            .then((data) => {
                // keys to preseve
                var k = [
                    ["id", ""], ["name", ""], ["creationTime", new Date()], ["lastModified", new Date()],
                    ["company", { name: "" }], ["price", 0], ["ageRestriction", ""]
                ];
                data = data.map((n) => {
                    var o = {};
                    k.forEach((l) => {
                        var v = _get(n, l[0], l[1]); // get the value, or set the default
                        o[l[0]] = isNull(v) ? l[1] : v;
                    });
                    o["price_"] = numeral(o["price"]).format("$ 0,0.00");
                    o["company_"] = o["company"]["name"];
                    o["creationTime_"] = moment(o["creationTime"]).format("MMM DD YYYY, hh:mm:ss a");
                    o["lastModified_"] = moment(o["lastModified"]).format("MMM DD YYYY, hh:mm:ss a");
                    return o;
                });
                this.$scope.gridOptions.data = data;
            },
            (msg) => {
                this.SweetAlert.alert(`Error code: ${msg}`, { title: "Error!" });
            });
    }
}

export default ProductsListController;
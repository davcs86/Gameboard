"use strict";

class RootController {
    constructor($scope, $state, $rootScope) {
        "ngInject";
        var vm = this;
        this.pageTitle = "Gameboard";
        this.sectionTitle = "Welcome";

        $rootScope.$on("title_updated",
        (evt, titles) => {
            vm.pageTitle = titles.pageTitle;
            vm.sectionTitle = titles.sectionTitle;
        });
    }
}

export default RootController;
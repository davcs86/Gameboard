"use strict";

class RootController {
    constructor($scope, $state, $rootScope) {
        "ngInject";
        var vm = this;
        this.pageTitle = "Gameboard";
        this.sectionTitle = "Welcome";
        this.$state = $state;

        $rootScope.$on("title_updated",
        (evt, titles) => {
            vm.pageTitle = titles.pageTitle;
            vm.sectionTitle = titles.sectionTitle;
        });
    }
    stateIncludes(state) {
        return !!this.$state.$current.includes[state];
    }
}

export default RootController;
"use strict";

class RootController {
    constructor($scope, $state, $rootScope/*, $log*/) {
        "ngInject";
        var vm = this;
        this.pageTitle = "Gameboard";
        this.sectionTitle = "Welcome";
        this.hidePanelLoading = true;
        this.$state = $state;
        //this.$log = $log;

        $rootScope.$on("title_updated", (evt, titles) => {
            vm.pageTitle = titles.pageTitle;
            vm.sectionTitle = titles.sectionTitle;
        });

        $rootScope.$on("page_loading", (evt, isLoading) => {
            vm.hidePanelLoading = !isLoading;
            //vm.$log.log("Loading: " + (isLoading ? "yep" : "nope"));
        });
    }
    stateIncludes(state) {
        return !!this.$state.$current.includes[state];
    }
}

export default RootController;
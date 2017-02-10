"use strict";

// vendor files
import "./setup.vendor";

// main App module
import "./setup.module";

angular.element(document)
    .ready(function() {
        angular.bootstrap(document,
        ["davcs86_gameboard"],
        {
            strictDi: true
        });
    });
"use strict";


import Angular from "angular";
import Collapse from "angular-ui-bootstrap/src/collapse";
import UiRouter from "angular-ui-router";
import RootSection from "../sections/root";
import CompaniesSection from "../sections/companies";
import Config from "./setup.config";

export default Angular.module(
        "davcs86_gameboard",
        [
            // plugins
            UiRouter,
            "ngAnimate",
            "ngTouch",
            "ngSanitize",
            "ngMessages",
            //"ngAria",
            "schemaForm",
            "ng-sweet-alert",
            "ui.grid",
            "ui.grid.autoResize",
            Collapse,

            // sections
            RootSection,
            CompaniesSection
        ]
    )
    .config(Config)
    .constant("$apiUrl", "/api");
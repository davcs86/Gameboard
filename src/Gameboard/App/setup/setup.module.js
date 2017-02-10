"use strict";


import Angular from "angular";
import Collapse from "angular-ui-bootstrap/src/collapse";
import UiRouter from "angular-ui-router";
import RootSection from "../sections/root";
import CompaniesSection from "../sections/companies";
import ProductsSection from "../sections/products";
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
            "schemaForm",
            "ng-sweet-alert",
            "ui.grid",
            "ui.grid.autoResize",
            "ui.grid.pagination",
            "ui.select",
            "ui.sortable",

            Collapse,

            // sections
            RootSection,
            CompaniesSection,
            ProductsSection
        ]
    )
    .config(Config)
    .constant("$apiUrl", "/api");
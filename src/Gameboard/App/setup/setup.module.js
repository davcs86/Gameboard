"use strict";

import * as Config from "./setup.config";
import * as Run from "./setup.run";
import Collapse from "angular-ui-bootstrap/src/collapse";
import UiRouter from "angular-ui-router";
import Routes from "./setup.routes";

const app = angular.module(
    "davcs86_gameboard",
    [
        // plugins
        UiRouter,
        "ngAnimate",
        "ngTouch",
        "ngSanitize",
        "ngMessages",
        "ngAria",
        "schemaForm",
        "toaster",
        Collapse,

        // routes
        Routes,

        // sections
        //require("./pages/events/events.module").name,
        //require("./pages/auth/auth.module").name
    ]
);

app
    .config(Config)
    .run(Run);


export default app;
"use strict";

import Angular from "angular";
import UiRouter from "angular-ui-router";
import RootController from "./root.controller";
import "./root.style";

export default Angular.module("davcs86_gameboard_root", [UiRouter])
    .controller("rootController", RootController)
    .name;
"use strict";

import Angular from "angular";
import RootController from "./root.controller";
import "./root.style";

export default Angular.module("davcs86_gameboard_root", [])
    .controller("rootController", RootController)
    .name;
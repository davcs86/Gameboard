"use strict";

import Angular from "angular";
import UiRouter from "angular-ui-router";
import Routes from "./companies.routes";
import CompaniesService from "./companies.service";

export default Angular.module("davcs86_gameboard_companies", [UiRouter])
    .service(CompaniesService.name, CompaniesService)
    .config(Routes)
    .name;
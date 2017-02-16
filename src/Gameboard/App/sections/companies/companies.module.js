"use strict";

import Angular from "angular";
import Routes from "./companies.routes";
import CompaniesService from "./companies.service";

export default Angular.module("davcs86_gameboard_companies", [])
    .service(CompaniesService.name, CompaniesService)
    .config(Routes)
    .name;
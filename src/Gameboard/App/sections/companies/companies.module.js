"use strict";

import Angular from "angular";
import Routes from "./companies.routes";
import CompaniesService from "./companies.service";

import ListCtrl from "./list/list.controller";
import EditCtrl from "./edit/edit.controller";
import NewCtrl from "./new/new.controller";

export default Angular.module("davcs86_gameboard_companies", [])
    .service(CompaniesService.name, CompaniesService)
    .controller(ListCtrl.name, ListCtrl)
    .controller(EditCtrl.name, EditCtrl)
    .controller(NewCtrl.name, NewCtrl)
    .config(Routes)
    .name;
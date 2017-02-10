"use strict";

import Angular from "angular";
import Routes from "./products.routes";
import ProductsService from "./products.service";

import ListCtrl from "./list/list.controller";
import EditCtrl from "./edit/edit.controller";
import NewCtrl from "./new/new.controller";

export default Angular.module("davcs86_gameboard_products", [])
    .service(ProductsService.name, ProductsService)
    .controller(ListCtrl.name, ListCtrl)
    .controller(EditCtrl.name, EditCtrl)
    .controller(NewCtrl.name, NewCtrl)
    .config(Routes)
    .name;
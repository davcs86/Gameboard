"use strict";

import Angular from "angular";
import Routes from "./products.routes";
import ProductsService from "./products.service";

export default Angular.module("davcs86_gameboard_products", [])
    .service(ProductsService.name, ProductsService)
    .config(Routes)
    .name;
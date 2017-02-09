import CompaniesService from "../companies/companies.service";

class ProductsService extends CompaniesService {
    constructor($http, $apiUrl, $q) {
        "ngInject";
        super($http, $apiUrl, $q);
        this.baseUrl = "/products/";
    }
}

export default ProductsService;
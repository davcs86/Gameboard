class CompaniesService {
    constructor($http, $apiUrl, $q) {
        "ngInject";
        this.$http = $http;
        this.apiUrl = $apiUrl;
        this.baseUrl = "/companies/";
        this.$q = $q;
    }
    _doRequest(parameters) {
        // return a promise
        return this.$q((resolve, reject) => this.$http(parameters)
            .then(
            (response) => { // success
                resolve(response.data);
            },
            (response) => { // error
                reject(response.statusText);
            }));
    }
    readAll() {
        return this._doRequest({
            method: "GET",
            url: this.apiUrl + this.baseUrl
        });
    }
    readOne(id) {
        return this._doRequest({
            method: "GET",
            url: this.apiUrl + this.baseUrl + id
        });
    }
    create(data) {
        return this._doRequest({
            method: "POST",
            url: this.apiUrl + this.baseUrl,
            data: data
        });
    }
    update(id, data) {
        return this._doRequest({
            method: "PUT",
            url: this.apiUrl + this.baseUrl + id,
            data: data
        });
    }
    delete(id) {
        return this._doRequest({
            method: "DELETE",
            url: this.apiUrl + this.baseUrl + id
        });
    }
}

export default CompaniesService;
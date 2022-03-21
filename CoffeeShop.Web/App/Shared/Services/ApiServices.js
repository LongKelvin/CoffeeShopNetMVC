/// <reference path="../../../assets/admin/lib/angular.js/angular.js" />

(function (app) {
    app.service('ApiServices', ApiServices);

    ApiServices.$inject = [

        '$http'
    ];

    function ApiServices($http) {
        return {
            get: get
        }

        function get(url, params, success, failure) {
            $http.get(url, params).then(function (result) {
                success(result);
            }, function (error) {
                failure(error);
            });
        }
    }
})(angular.module('CoffeeShop.Common'));
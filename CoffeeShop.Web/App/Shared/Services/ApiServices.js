/// <reference path="../../../assets/admin/lib/angular.js/angular.js" />

(function (app) {
    app.service('ApiServices', ApiServices);

    ApiServices.$inject = [

        '$http',
        'NotificationService',
        'AuthenticationService'
    ];

    function ApiServices($http, NotificationService, AuthenticationService) {
        return {
            get: get,
            post: post,
            put: put,
            del: del
        }

        function get(url, params, success, failure) {
            AuthenticationService.setHeader();
            $http.get(url, params).then(function (result) {
                success(result);
            }, function (error) {
                failure(error);
            });
        }

        function del(url, data, success, failure) {
            AuthenticationService.setHeader();
            $http.delete(url, data).then(function (result) {
                success(result);
            }, function (error) {
                console.log(error.status)
                if (error.status === 401) {
                    NotificationService.displayError('Authenticate is required.');
                }
                else if (failure != null) {
                    failure(error);
                }
            });
        }
        function post(url, data, success, failure) {
            AuthenticationService.setHeader();
            $http.post(url, data).then(function (result) {
                success(result);
            }, function (error) {
                console.log(error.status)
                if (error.status === 401) {
                    NotificationService.displayError('Authenticate is required.');
                }
                else if (failure != null) {
                    failure(error);
                }
            });
        }
        function put(url, data, success, failure) {
            AuthenticationService.setHeader();
            $http.put(url, data).then(function (result) {
                success(result);
            }, function (error) {
                console.log(error.status)
                if (error.status === 401) {
                    NotificationService.displayError('Authenticate is required.');
                }
                else if (failure != null) {
                    failure(error);
                }
            });
        }
    }
})(angular.module('CoffeeShop.Common'));
/// <reference path="../assets/admin/lib/angular.js/angular.js" />

(function () {
    angular.module('CoffeeShop', [

        // Custom modules
        'CoffeeShop.Common',
        'CoffeeShop.Products',
        'CoffeeShop.ProductCategory',
        'CoffeeShop.Slides',

    ]).config(config)
        .config(configAuthentication);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('Base', {
                url: "",
                templateUrl: "/App/Shared/Views/BaseView.html",
                abstract: true,
            })
            .state('Login', {
                url: "/Login",
                templateUrl: "/App/Components/Login/LoginView.html",
                controller: "LoginController"
            })

            .state('Home', {
                url: "/Admin",
                parent: 'Base',
                templateUrl: "/App/Components/Home/HomeView.html",
                controller: "HomeController"
            });


        $urlRouterProvider.otherwise('/Login');
    }

    function configAuthentication($httpProvider) {
        $httpProvider.interceptors.push(function ($q, $location) {
            return {
                request: function (config) {
                    return config;
                },
                requestError: function (rejection) {
                    return $q.reject(rejection);
                },
                response: function (response) {
                    if (response.status == "401") {
                        $location.path('/Login');
                    }
                    //the same response/modified/or a new one need to be returned.
                    return response;
                },
                responseError: function (rejection) {
                    if (rejection.status == "401") {
                        $location.path('/Login');
                    }
                    return $q.reject(rejection);
                }
            };
        });
    }
})();
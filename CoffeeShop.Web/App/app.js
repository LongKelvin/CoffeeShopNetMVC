/// <reference path="../assets/admin/lib/angular.js/angular.js" />

(function () {
    angular.module('CoffeeShop', [

        // Custom modules
        'CoffeeShop.Common',
        'CoffeeShop.Products',
        'CoffeeShop.ProductCategory',

    ]).config(config);

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
})();
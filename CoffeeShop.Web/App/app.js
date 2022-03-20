/// <reference path="../assets/admin/lib/angular.js/angular.js" />

(function () {
    angular.module('CoffeeShop', [

        // Custom modules
        'CoffeeShop.Common',
        'CoffeeShop.Products',
        /*'CoffeeShop.ProductCategory'*/
        'CoffeeShop.ProductCategory'

    ]).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('Home', {
            url: "/Admin",
            templateUrl: "/App/Components/Home/HomeView.cshtml",
            controller: "HomeController"
        });

        $urlRouterProvider.otherwise('/Admin')
    }
})();
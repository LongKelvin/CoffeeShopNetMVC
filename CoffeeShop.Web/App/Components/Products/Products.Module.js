/// <reference path="../../../assets/admin/lib/angular.js/angular.js" />

(function () {
    angular.module('CoffeeShop.Products', [

        // Custom modules
        'CoffeeShop.Common'

    ]).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('Products', {
                url: "/Products",
                templateUrl: "/App/Components/Products/ProductListView.html",
                controller: "ProductListController"
            })
            .state('ProductAdd', {
                url: "/Products/Add",
                templateUrl: "/App/Components/Products/ProductAddView.html",
                controller: "ProductAddController"
            })
            .state('ProductEdit', {
                url: "/Products/Edit",
                templateUrl: "/App/Components/Products/ProductEditView.html",
                controller: "ProductEditController"
            });
    }
})();
/// <reference path="../../../assets/admin/lib/angular.js/angular.js" />

(function() {
    angular.module('CoffeeShop.ProductCategory', [

        // Custom modules
        'CoffeeShop.Common'

    ]).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];


    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('ProductCategory', {
                url: "/ProductCategory",
                templateUrl: "/App/Components/ProductCategories/ProductCategoryListView.cshtml",
                controller: "ProductListController"
            })
            .state('ProductCategoryAdd', {
                url: "/ProductCategory/Add",
                templateUrl: "/App/Components/ProductCategories/ProductCategoryAddView.cshtml",
                controller: "ProductAddController"
            })
            .state('ProductCategoryEdit', {
                url: "/ProductCategory/Edit",
                templateUrl: "/App/Components/ProductCategories/ProductCategoryEditView.cshtml",
                controller: "ProductEditController"
            });
    }
})();
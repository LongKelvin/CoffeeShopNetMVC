/// <reference path="../../../assets/admin/lib/angular.js/angular.js" />

(function () {
    angular.module('CoffeeShop.ProductCategory', [

        // Custom modules
        'CoffeeShop.Common',

    ]).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('ProductCategory', {
                url: "/ProductCategory",
                parent: 'Base',
                templateUrl: "/App/Components/ProductCategories/ProductCategoryListView.html",
                controller: "ProductCategoryListController"
            })
            .state('ProductCategoryAdd', {
                url: "/ProductCategory/Add",
                parent: 'Base',
                templateUrl: "/App/Components/ProductCategories/ProductCategoryAddView.html",
                controller: "ProductCategoryAddController"
            })
            .state('ProductCategoryEdit', {
                url: "/ProductCategory/Edit/:id",
                parent: 'Base',
                templateUrl: "/App/Components/ProductCategories/ProductCategoryEditView.html",
                controller: "ProductCategoryEditController"
            });
    }
})();
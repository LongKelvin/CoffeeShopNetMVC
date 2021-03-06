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
                parent: 'Base',
                templateUrl: "/App/Components/Products/ProductListView.html",
                controller: "ProductListController"
            })
            .state('ProductAdd', {
                url: "/Products/Add",
                parent: 'Base',
                templateUrl: "/App/Components/Products/ProductAddView.html",
                controller: "ProductAddController"
            })
            .state('ProductEdit', {
                url: "/Products/Edit/:id",
                parent: 'Base',
                templateUrl: "/App/Components/Products/ProductEditView.html",
                controller: "ProductEditController"
            })
            .state('ProductImport', {
                url: "/Products/ImportFromExcel",
                parent: 'Base',
                templateUrl: "/App/Components/Products/ProductExcelImportView.html",
                controller: "ProductExcelImportController"
            });
    }
})();
/// <reference path="../../shared/services/apiservices.js" />

(function (app) {
    app.controller('ProductCategoryListController', ProductCategoryListController);

    ProductCategoryListController.$inject = ['$scope', 'ApiServices'];

    function ProductCategoryListController($scope, ApiServices) {
        $scope.title = 'ProductCategoryListController';

        $scope.productCategories = [];

        $scope.getProductCagories = getProductCagories;

        function getProductCagories() {
            try {
                ApiServices.get('/api/ProductCategory/GetAll', null, function (result) {
                    $scope.productCategories = result.data;
                }, function () {
                    console.log('Load productcategory failed.');
                });
            }
            catch {
                console.log("Exception in getProductCategoies function")
            }
        }
        $scope.getProductCagories();
    }
})(angular.module('CoffeeShop.ProductCategory'));
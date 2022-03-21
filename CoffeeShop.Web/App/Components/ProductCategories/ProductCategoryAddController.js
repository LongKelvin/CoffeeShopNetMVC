(function(app) {
    'use strict';

    app.controller('ProductCategoryAddController', ProductCategoryAddController);

    ProductCategoryAddController.$inject = ['$scope'];

    function ProductCategoryAddController($scope) {
        $scope.title = 'ProductCategoryAddController';

        activate();

        function activate() {}
    }
})(angular.module('CoffeeShop.ProductCategory'));
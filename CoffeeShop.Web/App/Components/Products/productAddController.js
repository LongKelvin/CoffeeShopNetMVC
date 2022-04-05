(function (app) {
    'use strict';

    app.controller('ProductAddController', ProductAddController);

    ProductAddController.$inject = ['$scope'];

    function ProductAddController($scope) {
        $scope.title = 'ProductAddController';

        activate();

        function activate() { }
    }
})(angular.module('CoffeeShop.Products'));
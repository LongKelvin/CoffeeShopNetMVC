(function (app) {
   

    app.controller('ProductEditController', ProductEditController);

    ProductEditController.$inject = ['$scope'];

    function ProductEditController($scope) {
        $scope.title = 'ProductEditController';

        activate();

        function activate() { }
    }
})(angular.module('CoffeeShop.Products'));

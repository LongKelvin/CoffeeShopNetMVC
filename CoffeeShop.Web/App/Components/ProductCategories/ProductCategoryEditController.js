(function(app) {


    app.controller('ProductCategoryEditController', ProductCategoryEditController);

    ProductCategoryEditController.$inject = ['$scope'];

    function ProductCategoryEditController($scope) {
        $scope.title = 'ProductCategoryEditController';

        activate();

        function activate() {}
    }
})(angular.module('CoffeeShop.ProductCategory'));
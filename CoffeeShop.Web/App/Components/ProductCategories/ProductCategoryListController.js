(function(app) {
    app.controller('ProductCategoryListController', ProductCategoryListController);

    ProductCategoryListController.$inject = ['$scope'];

    function ProductCategoryListController($scope) {
        $scope.title = 'ProductCategoryListController';

        activate();

        function activate() {}
    }
})(angular.module('CoffeeShop.ProductCategory'));
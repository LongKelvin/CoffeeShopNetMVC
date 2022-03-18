(function () {
    'use strict';

    angular
        .module('app')
        .controller('productListController', productListController);

    productListController.$inject = ['$scope'];

    function productListController($scope) {
        $scope.title = 'productListController';

        activate();

        function activate() { }
    }
})();

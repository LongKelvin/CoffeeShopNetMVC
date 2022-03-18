(function () {
    'use strict';

    angular
        .module('app')
        .controller('ProductListController', ProductListController);

    ProductListController.$inject = ['$scope'];

    function ProductListController($scope) {
        $scope.title = 'ProductListController';

        activate();

        function activate() { }
    }
})();

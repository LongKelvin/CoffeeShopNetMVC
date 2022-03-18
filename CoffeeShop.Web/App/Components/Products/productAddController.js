(function () {
    'use strict';

    angular
        .module('app')
        .controller('productAddController', productAddController);

    productAddController.$inject = ['$scope'];

    function productAddController($scope) {
        $scope.title = 'productAddController';

        activate();

        function activate() { }
    }
})();

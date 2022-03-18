(function () {
    'use strict';

    angular
        .module('app')
        .controller('ProductAddController', ProductAddController);

    productAddController.$inject = ['$scope'];

    function productAddController($scope) {
        $scope.title = 'ProductAddController';

        activate();

        function activate() { }
    }
})();

(function () {
    'use strict';

    angular
        .module('app')
        .controller('ProductEditController', ProductEditController);

    ProductEditController.$inject = ['$scope'];

    function ProductEditController($scope) {
        $scope.title = 'ProductEditController';

        activate();

        function activate() { }
    }
})();

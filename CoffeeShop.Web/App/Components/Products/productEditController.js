(function () {
    'use strict';

    angular
        .module('app')
        .controller('productEditController', productEditController);

    productEditController.$inject = ['$scope'];

    function productEditController($scope) {
        $scope.title = 'productEditController';

        activate();

        function activate() { }
    }
})();

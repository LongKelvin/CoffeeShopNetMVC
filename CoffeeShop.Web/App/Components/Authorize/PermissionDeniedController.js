(function (app) {
    app.controller('PermissionDeniedController', PermissionDeniedController);

    PermissionDeniedController.$inject = ['$scope', 'LoginService'];

    function PermissionDeniedController($scope, LoginService) {
        $scope.title = 'PermissionDeniedController';

        $scope.getUserName = getUserName;

        function getUserName() {
            $scope.userName =  LoginService.getUserName();
        }


        $scope.getUserName();
    }
})(angular.module('CoffeeShop'));
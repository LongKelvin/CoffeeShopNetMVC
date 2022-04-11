(function (app) {
    app.controller('RootController', RootController);
    RootController.$inject = ['$scope', '$state']

    function RootController($scope, $state) {
        $scope.logOut = function () {
            $state.go('Login')
        }
    }
})(angular.module('CoffeeShop'))
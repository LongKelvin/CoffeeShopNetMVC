/// <reference path="../../shared/services/notificationservice.js" />
(function (app) {
    app.controller('LoginController', LoginController);

    LoginController.$inject = [
        '$scope',
        '$state',
        'ApiServices',
        'NotificationService',
        '$stateParams',
        '$http'

    ];

    function LoginController($scope, $state, ApiServices, NotificationService) {
        $scope.loginSubmit = function () {
            $state.go('Home');
        }
    }
})(angular.module('CoffeeShop'));
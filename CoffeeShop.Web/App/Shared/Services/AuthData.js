(function (app) {
    'use-strict';

    app.factory('AuthData', [
        function () {
            var authDataFactory = {};

            var authentication = {
                isAuthenticated: false,
                userName: ""
            };

            authDataFactory.authenticationData = authentication;

            return authDataFactory;
        }
    ]);
})(angular.module('CoffeeShop.Common'))
////(function (app) {
////    app.controller('RootController', RootController);
////    RootController.$inject = ['$scope', '$state', 'AuthData', 'LoginService', 'AuthenticationSerice']

////    function RootController($scope, $state, AuthData, LoginService, AuthenticationService) {
////        $scope.logOut = function () {
////            //LoginService.logOut();
////            $state.go('Login')
////        }
////        //$scope.authentication = AuthData.authenticationData;

////        //AuthenticationService.validateRequest();
////    }
////})(angular.module('CoffeeShop'))

    (function (app) {
        app.controller('RootController', RootController);
        RootController.$inject = ['$scope', '$state', 'AuthData', 'LoginService', 'AuthenticationService']

        function RootController($scope, $state, AuthData, LoginService, AuthenticationService) {
            $scope.logOut = function () {
                LoginService.logOut();
                console.log(AuthData.authenticationData)
                $state.go('Login')
            }
            $scope.authentication = AuthData.authenticationData;

            //AuthenticationService.validateRequest();
        }
    })(angular.module('CoffeeShop'))
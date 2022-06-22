(function (app) {
    app.controller('LoginController', ['$scope', '$injector', 'LoginService', '$location', 'NotificationService',
        function ($scope, $injector, LoginService, $location, NotificationService) {
            $scope.loginData = {
                userName: "",
                password: ""
            };

            $scope.loginSubmit = function () {
                LoginService.login($scope.loginData.userName, $scope.loginData.password)
                    .then(function (response) {
                        //console.log('login_response_data: ', response)
                        if (response != null && response.data.error != undefined) {
                            NotificationService.displayError(response.data.error);
                            //NotificationService.displayError(response.data.error_description);
                            //console.error("Login failed, UserName or Password incorrect");
                        }
                        else {
                            //var stateService = $injector.get('$state');
                            //stateService.go('Products');
                            $location.path('/Products');
                            //console.log("Login successfull");
                        }
                    });
            }
        }]);
})(angular.module('CoffeeShop'));
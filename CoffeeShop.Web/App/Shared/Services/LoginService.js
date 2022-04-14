(function (app) {
    'use strict';
    app.service('LoginService', ['$http', '$q', 'AuthenticationService', 'AuthData',
        function ($http, $q, AuthenticationService, AuthData) {
            var userInfo;
            var deferred;

            this.login = function (userName, password) {
                deferred = $q.defer();
                var data = "grant_type=password&username=" + userName + "&password=" + password;
                $http.post('/oauth/token', data, {
                    headers:
                        { 'Content-Type': 'application/x-www-form-urlencoded' }
                }).then(
                    function (response) {
                        userInfo = {
                            accessToken: response.data.access_token,
                            userName: userName
                        };
                        AuthenticationService.setTokenInfo(userInfo);
                        AuthData.authenticationData.isAuthenticated = true;
                        AuthData.authenticationData.userName = userName;
                        deferred.resolve(null);

                        //console.log('UserInfo_AfterLogin: ', userInfo)
                    },
                    function (err, status) {
                        AuthData.authenticationData.isAuthenticated = false;
                        AuthData.authenticationData.userName = "";
                        deferred.resolve(err);

                        // console.error('UserInfo_AfterLogin_HasError_Status: ', status)
                        //console.error('UserInfo_AfterLogin_HasError_Error: ', err)
                    }
                );

                return deferred.promise;
            }

            this.logOut = function () {
                AuthenticationService.removeToken();
                AuthData.authenticationData.isAuthenticated = false;
                AuthData.authenticationData.userName = "";
            }
        }]);
})(angular.module('CoffeeShop.Common'));
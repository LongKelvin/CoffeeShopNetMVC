(function (app) {
    'use strict';
    app.service('LoginService', ['$http', '$q', '$window', 'AuthenticationService', 'AuthData', 'ApiServices', 'NotificationService',
        function ($http, $q, $window, AuthenticationService, AuthData, ApiServices, NotificationService) {
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
                        AuthData.authenticationData.accessToken = userInfo.accessToken;

                        deferred.resolve(null);

                        //console.log('UserInfo_AfterLogin: ', userInfo)
                    },
                    function (error) {
                        console.log(error)
                        AuthData.authenticationData.IsAuthenticated = false;
                        AuthData.authenticationData.userName = "";
                        deferred.resolve(error);
                    }
                );

                return deferred.promise;
            }

            this.logOut = function () {
                ApiServices.post('api/Account/Logout', null, function (response) {
                    //console.log('response from server: ', response)

                    if (response.data.success == true) {
                        AuthenticationService.removeToken();
                        AuthData.authenticationData.IsAuthenticated = false;
                        AuthData.authenticationData.userName = "";
                        AuthData.authenticationData.accessToken = "";
                        $window.sessionStorage["TokenInfo"] = null;
                        $window.sessionStorage.clear();
                        $window.localStorage.clear();
                    }
                }, null);
            }

            this.getUserInfo = function () {
                return userInfo;
            }

            this.getUserName = function () {
                return userInfo;
            }
        }]);
})(angular.module('CoffeeShop.Common'));
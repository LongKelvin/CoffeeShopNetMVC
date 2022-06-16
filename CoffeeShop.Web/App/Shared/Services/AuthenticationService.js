(function (app) {
    'use strict';

    app.service('AuthenticationService', ['$http', '$q', '$window','$location', 'AuthData',
        function ($http, $q, $window, $location, AuthData) {
            var tokenInfo;

            this.setTokenInfo = function (data) {
                //console.log('TokenData: ', data);
                tokenInfo = data;
                // console.log('TokenInfo_afterSet: ', tokenInfo);
                $window.sessionStorage["TokenInfo"] = JSON.stringify(tokenInfo);
            }

            this.getTokenInfo = function () {
                return tokenInfo;
            }

            this.removeToken = function () {
                tokenInfo = null;
                $window.sessionStorage["TokenInfo"] = null;
            }

            this.init = function () {
                if ($window.sessionStorage["TokenInfo"]) {
                    tokenInfo = JSON.parse($window.sessionStorage["TokenInfo"]);
                    AuthData.authenticationData.IsAuthenticated = true;
                    AuthData.authenticationData.userName = tokenInfo.userName;
                    AuthData.authenticationData.accessToken = tokenInfo.accessToken;
                }
            }

            this.setHeader = function () {
               
                delete $http.defaults.headers.common['X-Requested-With'];
                if ((tokenInfo != undefined) && (tokenInfo.accessToken != undefined) && (tokenInfo.accessToken != null) && (tokenInfo.accessToken != "")) {
                    $http.defaults.headers.common['Authorization'] = 'Bearer ' + tokenInfo.accessToken;
                    $http.defaults.headers.common['Content-Type'] = 'application/x-www-form-urlencoded;charset=utf-8';

                }
                else {

                    
                    //This is a dirty way to remove the token info from header when call ApiService
                    //However when some method has call outside ApiService(using $http) then the trick
                    //cannot work as well as expect
                    
                    $http.defaults.headers.common['Authorization'] = '';
                    $http.defaults.headers.common['Content-Type'] = '';

                    //force return to Login page
                    $location.path('/Login');
                }
            }

            this.validateRequest = function () {
                var url = 'api/Home/TestMethod';
                var deferred = $q.defer();
                $http.get(url).then(function () {

                    deferred.resolve(null);
                }, function (error) {
                    console.log(error)
                    deferred.reject(error);
                });
                return deferred.promise;
            }

            this.init();
        }]);
}) (angular.module('CoffeeShop.Common'));
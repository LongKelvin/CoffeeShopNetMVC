(function (app) {
    'use strict';
    app.service('AuthenticationService', ['$http', '$q', '$window',
        function ($http, $q, $window) {
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
                    //console.log('Init $window.sessionStorage: ', tokenInfo)
                    //console.log(' $window.sessionStorage: ', ($window.sessionStorage["TokenInfo"]))
                }
            }

            this.setHeader = function () {
                //console.log('tokenInfo_beforeSetHeader: ', tokenInfo)
                delete $http.defaults.headers.common['X-Requested-With'];
                if ((tokenInfo != undefined) && (tokenInfo.accessToken != undefined) && (tokenInfo.accessToken != null) && (tokenInfo.accessToken != "")) {
                    $http.defaults.headers.common['Authorization'] = 'Bearer ' + tokenInfo.accessToken;
                    $http.defaults.headers.common['Content-Type'] = 'application/x-www-form-urlencoded;charset=utf-8';

                    //console.log('tokenInfo_InSetHeader: ', tokenInfo)
                }
            }

            this.validateRequest = function () {
                var url = 'api/Home/TestMethod';
                var deferred = $q.defer();
                $http.get(url).then(function () {
                    deferred.resolve(null);
                }, function (error) {
                    deferred.reject(error);
                });
                return deferred.promise;
            }

            this.init();
        }
    ]);
})(angular.module('CoffeeShop.Common'));
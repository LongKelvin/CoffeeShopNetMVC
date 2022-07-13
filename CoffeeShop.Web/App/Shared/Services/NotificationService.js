(function (app) {
    'use strict';
    app.service('NotificationService', NotificationService);

    NotificationService.$inject = ['$http'];

    function NotificationService($http) {
        toastr.options = {
            "debug": false,
            "positionClass": "toast-top-right",
            "onclick": null,
            "fadeIn": 500,
            "fadeOut": 1500,
            "timeOut": 8000,
            "extendedTimeOut": 3000
        };

        function displaySuccess(message) {

         
            toastr.success(message);
        }

        function displayError(error) {
            if (Array.isArray(error)) {
                error.each(function (err) {
                    toastr.error(err);
                });
            }
            else {
                toastr.error(error);
            }
        }

        function displayWarning(message) {
            toastr.warning(message);
        }
        function displayInfo(message) {
            toastr.info(message);
        }

        return {
            displaySuccess: displaySuccess,
            displayError: displayError,
            displayWarning: displayWarning,
            displayInfo: displayInfo
        }
    }
})(angular.module('CoffeeShop.Common'));
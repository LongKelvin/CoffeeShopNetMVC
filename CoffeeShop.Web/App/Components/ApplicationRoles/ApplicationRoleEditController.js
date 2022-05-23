(function (app) {
    'use strict';

    app.controller('ApplicationRoleEditController', ApplicationRoleEditController);

    ApplicationRoleEditController.$inject = ['$scope', 'ApiService', 'NotificationService', '$location', '$stateParams'];

    function ApplicationRoleEditController($scope, ApiService, NotificationService, $location, $stateParams) {
        $scope.role = {}


        $scope.updateApplicationRole = updateApplicationRole;

        function updateApplicationRole() {
            ApiService.put('/api/ApplicationRole/Update', $scope.role, addSuccessed, addFailed);
        }
        function loadDetail() {
            ApiService.get('/api/ApplicationRole/Detail/' + $stateParams.id, null,
                function (result) {
                    $scope.role = result.data;
                },
                function (result) {
                    NotificationService.displayError(result.data);
                });
        }

        function addSuccessed() {
            NotificationService.displaySuccess($scope.role.Name + ' đã được cập nhật thành công.');

            $location.url('ApplicationRoles');
        }
        function addFailed(response) {
            NotificationService.displayError(response.data.Message);
            NotificationService.displayErrorValidation(response);
        }
        loadDetail();
    }
})(angular.module('CoffeeShop.ApplicationRoles'));
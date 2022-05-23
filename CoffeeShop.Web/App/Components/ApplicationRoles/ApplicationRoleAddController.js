(function (app) {
    'use strict';

    app.controller('ApplicationRoleAddController', ApplicationRoleAddController);

    ApplicationRoleAddController.$inject = ['$scope', 'ApiService', 'NotificationService', '$location', 'CommonService'];

    function ApplicationRoleAddController($scope, ApiService, NotificationService, $location, CommonService) {
        $scope.role = {
            Id: 0
        }

        $scope.addAppRole = addAppRole;

        function addAppRole() {
            ApiService.post('/api/ApplicationRole/Add', $scope.role, addSuccessed, addFailed);
        }

        function addSuccessed() {
            NotificationService.displaySuccess($scope.role.Name + ' đã được thêm mới.');

            $location.url('application_roles');
        }
        function addFailed(response) {
            NotificationService.displayError(response.data.Message);
            NotificationService.displayErrorValidation(response);
        }

        function loadRoles() {
            ApiService.get('/api/applicationRole/getlistall',
                null,
                function (response) {
                    $scope.roles = response.data;
                }, function (response) {
                    NotificationService.displayError('Không tải được danh sách quyền.');
                });

        }

        loadRoles();
    }
})(angular.module('CoffeeShop.ApplicationRoles'));
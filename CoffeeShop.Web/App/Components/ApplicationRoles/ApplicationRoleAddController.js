(function (app) {
    'use strict';

    app.controller('ApplicationRoleAddController', ApplicationRoleAddController);

    ApplicationRoleAddController.$inject = ['$scope', 'ApiServices', 'NotificationService', '$location'];

    function ApplicationRoleAddController($scope, ApiServices, NotificationService, $location) {
        $scope.role = {
            Id: 0
        }

        $scope.AddApplicationRole = AddApplicationRole;

        function AddApplicationRole() {
            ApiServices.post('/api/ApplicationRole/Add', $scope.role, AddSuccessed, AddFailed);
        }

        function AddSuccessed() {
            NotificationService.displaySuccess($scope.role.Name + ' đã được thêm mới.');

            $location.url('ApplicationRoles');
        }
        function AddFailed(response) {
            NotificationService.displayError(response.data.Message);
            NotificationService.displayErrorValidation(response);
        }

        function LoadRoles() {
            ApiServices.get('/api/applicationRole/getlistall',
                null,
                function (response) {
                    $scope.roles = response.data;
                }, function (response) {
                    NotificationService.displayError('Không tải được danh sách quyền.');
                });
        }

        LoadRoles();
    }
})(angular.module('CoffeeShop.ApplicationRoles'));
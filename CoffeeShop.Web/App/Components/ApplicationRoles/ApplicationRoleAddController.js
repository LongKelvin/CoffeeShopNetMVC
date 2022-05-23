(function (app) {
    'use strict';

    app.controller('ApplicationRoleAddController', ApplicationRoleAddController);

    ApplicationRoleAddController.$inject = ['$scope', 'ApiServices', 'NotificationService', '$location', 'CommonService'];

    function ApplicationRoleAddController($scope, ApiServices, NotificationService, $location, CommonService) {
        $scope.role = {
            Id: 0
        }

        $scope.addAppRole = addAppRole;

        function addAppRole() {
            ApiServices.post('/api/ApplicationRole/Add', $scope.role, addSuccessed, addFailed);
        }

        function addSuccessed() {
            NotificationService.displaySuccess($scope.role.Name + ' đã được thêm mới.');

            $location.url('ApplicationRoles');
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
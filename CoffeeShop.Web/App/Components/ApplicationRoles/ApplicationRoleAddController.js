(function (app) {
    'use strict';

    app.controller('ApplicationRoleAddController', ApplicationRoleAddController);

    ApplicationRoleAddController.$inject = ['$scope', 'ApiServices', 'NotificationService', '$location'];

    function ApplicationRoleAddController($scope, ApiServices, NotificationService, $location) {
        $scope.role = {
            Id: 0,
            PermissionIds: []
        }
        $scope.permissions = [];
        $scope.listPermissionId = [];

        $scope.AddApplicationRole = AddApplicationRole;
        $scope.LoadAllAppPermission = LoadAllAppPermission;

        function AddApplicationRole() {
            GetPermissions();
            console.log($scope.roles)
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

        function LoadAllAppPermission() {
            ApiServices.get('/api/ApplicationPermission/GetListAll',
                null,
                function (response) {
                    $scope.permissions = response.data;
                    console.log('app permission list: ', $scope.permissions)
                }, function (response) {
                    NotificationService.displayError('Không tải được danh sách permissions.');
                });
        }

        function GetPermissions() {
            $("input:checkbox[class=checkbox-permission]:checked").each(function () {
                //$scope.listPermissionId.push($(this).val())
                $scope.role.PermissionIds.push($(this).val())
            });
        }

        LoadRoles();
        LoadAllAppPermission();
    }
})(angular.module('CoffeeShop.ApplicationRoles'));
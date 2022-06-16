(function (app) {
    'use strict';

    app.controller('ApplicationRoleEditController', ApplicationRoleEditController);

    ApplicationRoleEditController.$inject = ['$scope', 'ApiServices', 'NotificationService', '$location', '$stateParams'];

    function ApplicationRoleEditController($scope, ApiServices, NotificationService, $location, $stateParams) {
        $scope.role = {
            PermissionIds: []
        }
        $scope.listAppPermissions = [];

        $scope.UpdateApplicationRole = UpdateApplicationRole;

        function UpdateApplicationRole() {
            getPermissions();
            ApiServices.put('/api/ApplicationRole/Update', $scope.role, addSuccessed, addFailed);
        }
        function loadDetail() {
            ApiServices.get('/api/ApplicationRole/Detail/' + $stateParams.id, null,
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

        function loadAllAppPermission() {
            ApiServices.get('/api/ApplicationPermission/GetListAll',
                null,
                function (response) {
                    $scope.listAppPermissions = response.data;
                    
                }, function (response) {
                    NotificationService.displayError('Không tải được danh sách permissions.');
                });
        }

        function loadAppPermissionForRole() {
            ApiServices.get('/api/ApplicationPermission/GetPermissionByRoleId/' + $stateParams.id,
                null,
                function (response) {
                    $scope.listPermissionByRole = response.data;
                    $.each(result.data, function (i, permission) {
                        $('#permission_' + permission.Name).prop('checked', true);
                    });

                }, function (response) {
                    NotificationService.displayError('Không tải được danh sách permissions.');
                });
        }

        function getPermissions() {
            $("input:checkbox[class=checkbox-permission]:checked").each(function () {
                //$scope.listPermissionId.push($(this).val())
                $scope.role.PermissionIds.push($(this).val())
            });
        }

        loadDetail();
        loadAllAppPermission();
        loadAppPermissionForRole();
    }
})(angular.module('CoffeeShop.ApplicationRoles'));
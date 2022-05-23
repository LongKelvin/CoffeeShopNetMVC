(function (app) {
    app.controller('ApplicationGroupEditController', ApplicationGroupEditController);

    ApplicationGroupEditController.$inject =
        [
            '$scope',
            'ApiServices',
            '$state',
            'NotificationService',
            '$stateParams',
        ];

    function ApplicationGroupEditController($scope, ApiServices, $state, NotificationService, $stateParams) {
        $scope.group = {}

        $scope.UpdateApplicationGroup = UpdateApplicationGroup;

        function UpdateApplicationGroup() {
            GetRoles();
            ApiServices.put('/api/applicationGroup/update', $scope.group, addSuccessed, addFailed);
        }
        function loadDetail() {
            ApiServices.get('/api/applicationGroup/detail/' + $stateParams.id, null,
                function (result) {
                    $scope.group = result.data;
                    console.log(result.data)
                    $.each(result.data.Roles, function (i, role) {
                        $('#role_' + role.Name).prop('checked', true);
                    });
                },
                function (result) {
                    NotificationService.displayError(result.data);
                });
        }

        function addSuccessed() {
            NotificationService.displaySuccess($scope.group.Name + ' đã được cập nhật thành công.');

            $state.go('ApplicationGroups');
        }
        function addFailed(response) {
            NotificationService.displayError(response.data.Message);
            NotificationService.displayErrorValidation(response);
        }
        function loadRoles() {
            ApiServices.get('/api/applicationRole/getlistall',
                null,
                function (response) {
                    $scope.roles = response.data;
                }, function (response) {
                    NotificationService.displayError('Không tải được danh sách quyền.');
                });
        }

        function GetRoles() {
            $("input:checkbox[class=checkbox-role]:checked").each(function () {
                var role = {
                    "Id": $(this).val(),
                    "Description": $(this).data("role-description"),
                    "Name": $(this).data("role-name")
                }
                $scope.group.Roles.push(role);
                console.log('Roles data: ', $scope.group.Roles)
            });
        }

        loadRoles();
        loadDetail();
    }
})(angular.module('CoffeeShop.ApplicationGroups'));
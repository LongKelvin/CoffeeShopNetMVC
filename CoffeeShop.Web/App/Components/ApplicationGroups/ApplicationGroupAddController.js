(function (app) {
    'use strict';

    app.controller('ApplicationGroupAddController', ApplicationGroupAddController);

    ApplicationGroupAddController.$inject = ['$scope', 'ApiServices', '$state', 'NotificationService', 'CommonService'];

    function ApplicationGroupAddController($scope, ApiServices, $state, NotificationService, CommonService) {
        $scope.title = 'ApplicationGroupAddController';

        $scope.group = {
            ID: 0,
            Roles: []
        }

        $scope.AddApplicationGroup = AddApplicationGroup;

        function AddApplicationGroup() {
            //console.log('group data: ', $scope.group)
            GetRoles();
            ApiServices.post('/api/ApplicationGroup/Add', $scope.group, AddSuccessed, AddFailed);
        }

        function GetRoles() {
            $("input:checkbox[class=checkbox-role]:checked").each(function () {
                var role = {
                    "Id": $(this).val(),
                    "Description": $(this).data("role-description"),
                    "Name": $(this).data("role-name")
                }
                $scope.group.Roles.push(role);
                //console.log('Roles data: ', $scope.group.Roles)
            });
        }

        function AddSuccessed() {
            NotificationService.displaySuccess($scope.group.Name + ' đã được thêm mới.');

            $state.go('ApplicationGroups');
        }
        function AddFailed(response) {
            NotificationService.displayError(response.data.Message);
            //NotificationService.displayError(response);
        }
        function LoadRoles() {
            ApiServices.get('/api/ApplicationRole/GetListAll',
                null,
                function (response) {
                    $scope.roles = response.data;
                    //console.log('all roles: ', response)
                }, function (response) {
                    NotificationService.displayError('Không tải được danh sách quyền.');
                });
        }

        LoadRoles();
    }
})(angular.module('CoffeeShop.ApplicationGroups'));
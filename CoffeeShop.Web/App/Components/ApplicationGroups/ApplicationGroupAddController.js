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
            ApiServices.post('/api/ApplicationGroup/Add', $scope.group, AddSuccessed, AddFailed);
        }

        function AddSuccessed() {
            NotificationService.displaySuccess($scope.group.Name + ' đã được thêm mới.');

            $location.url('ApplicationGroups');
        }
        function AddFailed(response) {
            NotificationService.displayError(response.data.Message);
            NotificationService.displayErrorValidation(response);
        }
        function LoadRoles() {
            ApiServices.get('/api/ApplicationRole/GetListAll',
                null,
                function (response) {
                    $scope.roles = response.data;
                    console.log('all roles: ', response)
                }, function (response) {
                    NotificationService.displayError('Không tải được danh sách quyền.');
                });

        }

        LoadRoles();
    }
})(angular.module('CoffeeShop.ApplicationGroups'));
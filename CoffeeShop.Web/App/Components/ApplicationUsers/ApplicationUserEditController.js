(function (app) {
    'use strict';

    app.controller('ApplicationUserEditController', ApplicationUserEditController);

    ApplicationUserEditController.$inject = ['$scope', 'ApiService', 'NotificationService', '$location', '$stateParams'];

    function ApplicationUserEditController($scope, ApiService, NotificationService, $location, $stateParams) {
        $scope.account = {}

        $scope.updateAccount = updateAccount;

        function updateAccount() {
            ApiService.put('/api/applicationUser/update', $scope.account, addSuccessed, addFailed);
        }
        function loadDetail() {
            ApiService.get('/api/applicationUser/detail/' + $stateParams.id, null,
                function (result) {
                    $scope.account = result.data;
                },
                function (result) {
                    NotificationService.displayError(result.data);
                });
        }

        function addSuccessed() {
            NotificationService.displaySuccess($scope.account.FullName + ' đã được cập nhật thành công.');

            $location.url('ApplicationUsers');
        }
        function addFailed(response) {
            NotificationService.displayError(response.data.Message);
            NotificationService.displayErrorValidation(response);
        }
        function loadGroups() {
            ApiService.get('/api/ApplicationGroup/GetListAll',
                null,
                function (response) {
                    $scope.groups = response.data;
                }, function (response) {
                    NotificationService.displayError('Không tải được danh sách nhóm.');
                });
        }

        loadGroups();
        loadDetail();
    }
})(angular.module('CoffeeShop.ApplicationUsers'));
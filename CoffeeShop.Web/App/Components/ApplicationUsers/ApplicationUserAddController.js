(function (app) {
    'use strict';

    app.controller('ApplicationUserAddController', ApplicationUserAddController);

    ApplicationUserAddController.$inject = ['$scope', 'ApiService', 'NotificationService', '$location', 'CommonService'];

    function ApplicationUserAddController($scope, ApiService, NotificationService, $location, CommonService) {
        $scope.account = {
            Groups: []
        }

        $scope.addAccount = addAccount;

        function addAccount() {
            ApiService.post('/api/ApplicationUser/Add', $scope.account, addSuccessed, addFailed);
        }

        function addSuccessed() {
            NotificationService.displaySuccess($scope.account.Name + ' đã được thêm mới.');

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

    }
})(angular.module('CoffeeShop.ApplicationUsers'));
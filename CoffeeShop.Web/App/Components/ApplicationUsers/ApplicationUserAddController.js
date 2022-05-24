(function (app) {
    'use strict';

    app.controller('ApplicationUserAddController', ApplicationUserAddController);

    ApplicationUserAddController.$inject = ['$scope', 'ApiServices', 'NotificationService', '$location', 'CommonService'];

    function ApplicationUserAddController($scope, ApiServices, NotificationService, $location, CommonService) {
        $scope.user = {
            Groups: []
        }

        $scope.AddApplicationUser = AddApplicationUser;

        function AddApplicationUser() {
            getCheckedGroup();
            console.log('submit data', $scope.user);
            ApiServices.post('/api/ApplicationUser/Add', $scope.user, addSuccessed, addFailed);
        }

        function addSuccessed() {
            NotificationService.displaySuccess($scope.user.Name + ' đã được thêm mới.');
            $location.url('ApplicationUsers');
        }
        function addFailed(response) {
            NotificationService.displayError(response.data.Message);
            //NotificationService.displayErrorValidation(response);
        }

        function getCheckedGroup() {
            $("input:checkbox[class=checkbox-group]:checked").each(function () {
                var gr = {
                    "Id": $(this).val(),
                    "Description": $(this).data("group-description"),
                    "Name": $(this).data("group-name")
                }
                $scope.user.Groups.push(gr);
                //console.log('Roles data: ', $scope.group.Roles)
            });
        }

        function loadGroups() {
            ApiServices.get('/api/ApplicationGroup/GetListAll',
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
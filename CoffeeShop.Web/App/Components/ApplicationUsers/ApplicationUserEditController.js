(function (app) {
    'use strict';

    app.controller('ApplicationUserEditController', ApplicationUserEditController);

    ApplicationUserEditController.$inject = ['$scope', 'ApiServices', 'NotificationService', '$location', '$stateParams'];

    function ApplicationUserEditController($scope, ApiServices, NotificationService, $location, $stateParams) {
        $scope.user = {}

        $scope.UpdateApplicationUser = UpdateApplicationUser;

        function UpdateApplicationUser() {
            getCheckedGroup();
            ApiServices.put('/api/applicationUser/update', $scope.user, addSuccessed, addFailed);
        }
        function loadDetail() {
            ApiServices.get('/api/applicationUser/detail/' + $stateParams.id, null,
                function (result) {
                    $scope.user = result.data;
                    $.each(result.data.Groups, function (i, gr) {
                        $('#gr_' + gr.Name).prop('checked', true);
                    });

                    if ($scope.user.BirthDay != null)
                        $scope.user.BirthDay = new Date($scope.user.BirthDay); 

                    console.log('Get DATA: ', $scope.user)
                },
                function (result) {
                    NotificationService.displayError(result.data);
                });
        }

        function addSuccessed() {
            NotificationService.displaySuccess($scope.user.FullName + ' đã được cập nhật thành công.');

            $location.url('ApplicationUsers');
        }
        function addFailed(response) {
            NotificationService.displayError(response.data.Message);
            NotificationService.displayErrorValidation(response);
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

        loadGroups();
        loadDetail();
    }
})(angular.module('CoffeeShop.ApplicationUsers'));
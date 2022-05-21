
(function (app) {
    app.controller('ApplicationGroupEditController', ApplicationGroupEditController);

    ApplicationGroupEditController.$inject =
        [
            '$scope',
            'ApiServices',
            '$state',
            'NotificationService',
            '$stateParams',
            'CommonService'
        ];

    function ApplicationGroupEditController($scope, ApiServices, $state, NotificationService, $stateParams, CommonService) {
        $scope.title = 'ApplicationGroupEditController';

        $scope.applicationGroup = {
            UpdatedDate: new Date(),
            UpdatedBy: 'AdminTest'
        }

        $scope.UpdateApplicationGroup = UpdateApplicationGroup;
       
        function UpdateApplicationGroup() {
            $scope.applicationGroup.UpdatedDate = new Date();
            $scope.applicationGroup.UpdatedBy = 'AdminTest';

            console.log('update data:', $scope.applicationGroup)
            ApiServices.post('api/ApplicationGroup/Update', $scope.applicationGroup,
                function (result) {
                    NotificationService.displaySuccess(result.data.Name + ' đã cập nhật thành công.');
                    $state.go('ApplicationGroups');
                }, function (error) {
                    console.error(error)
                    NotificationService.displayError('Đã có lỗi xảy ra, Xin vui lòng thử lại.');
                });
            $state.go('ApplicationGroups')
        }

        function loadApplicationGroupDetail() {
            ApiServices.get('api/ApplicationGroup/GetById/' + $stateParams.id, null, function (result) {
                $scope.applicationGroup = result.data;

                
                console.log('ApplicationGroupData: ', $scope.applicationGroup)
            }, function (error) {
                NotificationService.displayError(error.data);
            });
        }
        loadApplicationGroupDetail();
    }
})(angular.module('CoffeeShop.ApplicationGroups'));
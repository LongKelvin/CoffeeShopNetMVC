(function (app) {
    app.controller('SlideEditController', SlideEditController);

    SlideEditController.$inject =
        [
            '$scope',
            'ApiServices',
            '$state',
            'NotificationService',
            '$stateParams',
            'CommonService'
        ];

    function SlideEditController($scope, ApiServices, $state, NotificationService, $stateParams, CommonService) {
        $scope.title = 'SlideEditController';

        $scope.slide = {
        }

        $scope.UpdateSlide = UpdateSlide;

        function UpdateSlide() {
            ApiServices.post('api/Slide/Update', $scope.slide,
                function (result) {
                    NotificationService.displaySuccess(result.data.Title + ' đã cập nhật thành công.');
                    $state.go('Slide');
                }, function (error) {
                    console.error(error)
                    NotificationService.displayError('Đã có lỗi xảy ra, Xin vui lòng thử lại.');
                });

            $state.go('Slides')
        }

        function loadSlideDetail() {
            ApiServices.get('api/Slide/GetById/' + $stateParams.id, null, function (result) {
                $scope.slide = result.data;

                console.log('SlideData: ', $scope.slide)
            }, function (error) {
                NotificationService.displayError(error.data);
            });
        }

        $scope.ChooseImages = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.Slide.Images = fileUrl;
                $scope.$apply();
                console.log('file url: ', fileUrl)
            }
            finder.popup();
        }

        loadSlideDetail();
    }
})(angular.module('CoffeeShop.Slides'));
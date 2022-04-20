(function (app) {
    'use strict';

    app.controller('SlideAddController', SlideAddController);

    SlideAddController.$inject = ['$scope', 'ApiServices', '$state', 'NotificationService', 'CommonService'];

    function SlideAddController($scope, ApiServices, $state, NotificationService, CommonService) {
        $scope.title = 'SlideAddController';

        $scope.slide = {
            Status: true
        }

        $scope.AddSlide = AddSlide;

        function AddSlide() {
            //var content = CKEDITOR.instances['ckEditorContent'].getData();
            //$scope.slide.Content = content;

            ApiServices.post('api/Slide/Create', $scope.slide,
                function (result) {
                    NotificationService.displaySuccess(result.data.Title + ' đã được thêm mới.');
                }, function (error) {
                    console.error(error)
                    NotificationService.displayError('Đã có lỗi xảy ra, Xin vui lòng thử lại.');
                });
            $state.go('Slides')
        }
      

        $scope.ChooseImages = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.slide.Images = fileUrl;
                $scope.$apply();
                console.log('file url: ', fileUrl)
            }
            finder.popup();
        }

    }
})(angular.module('CoffeeShop.Slides'));
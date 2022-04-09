
(function (app) {
    'use strict';

    app.controller('ProductAddController', ProductAddController);

    ProductAddController.$inject = ['$scope', 'ApiServices', '$state', 'NotificationService', 'CommonService'];

    function ProductAddController($scope, ApiServices, $state, NotificationService, CommonService) {
        $scope.title = 'ProductAddController';

        $scope.product = {
            Status: true,
            CreatedDate: new Date(),
            HomeFlag: false,
            CreatedBy: 'AdminTest'
        }

        $scope.AddProduct = AddProduct;
        $scope.getSeoTitle = getSeoTitle;
       

        function getSeoTitle() {
            $scope.product.Alias = CommonService.getSeoTitle($scope.product.Name);
        }

        function AddProduct() {
            var content = CKEDITOR.instances['ckEditorContent'].getData();
            $scope.product.Content = content; 

            ApiServices.post('api/Product/Create', $scope.product,
                function (result) {
                    NotificationService.displaySuccess(result.data.Name + ' đã được thêm mới.');
                }, function (error) {
                    console.error(error)
                    NotificationService.displayError('Đã có lỗi xảy ra, Xin vui lòng thử lại.');
            });
            $state.go('Products')
        }
        function loadParentCategory() {
            ApiServices.get('api/ProductCategory/GetAllParents', null, function (result) {
                $scope.parentCategories = result.data;
            }, function () {
                console.log('Cannot get list parent');
            });
        }

        $scope.ChooseImages = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.product.Images = fileUrl;
                console.log('file url: ', fileUrl)
            }
            finder.popup();
        }

        loadParentCategory();
    }
})(angular.module('CoffeeShop.Products'));
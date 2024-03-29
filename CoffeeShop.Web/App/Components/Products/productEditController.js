﻿(function (app) {
    app.controller('ProductEditController', ProductEditController);

    ProductEditController.$inject =
        [
            '$scope',
            'ApiServices',
            '$state',
            'NotificationService',
            '$stateParams',
            'CommonService'
        ];

    function ProductEditController($scope, ApiServices, $state, NotificationService, $stateParams, CommonService) {
        $scope.title = 'ProductEditController';

        $scope.product = {
            UpdatedDate: new Date(),
            UpdatedBy: 'AdminTest'
        }

        $scope.UpdateProduct = UpdateProduct;
        $scope.getSeoTitle = getSeoTitle;
        $scope.moreImages = []

        function getSeoTitle() {
            $scope.product.Alias = CommonService.getSeoTitle($scope.product.Name);
        }

        function UpdateProduct() {
            $scope.product.UpdatedDate = new Date();
            $scope.product.UpdatedBy = 'AdminTest';
            $scope.product.MoreImages = JSON.stringify($scope.moreImages)

            //console.log($scope.product)

            ApiServices.post('api/Product/Update', $scope.product,
                function (result) {
                    NotificationService.displaySuccess(result.data.Name + ' đã cập nhật thành công.');
                    $state.go('Product');
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
                //console.log('Cannot get list parent');
            });
        }

        function loadProductDetail() {
            ApiServices.get('api/Product/GetById/' + $stateParams.id, null, function (result) {
                $scope.product = result.data;
                if ($scope.product.ManufacturingDate != null)
                    $scope.product.ManufacturingDate = new Date($scope.product.ManufacturingDate); // convert filed to date
                if ($scope.product.ExpireDate != null)
                    $scope.product.ExpireDate = new Date($scope.product.ExpireDate); // convert filed to date

                var checkMoreImagesValue = CommonService.isNullOrEmpty($scope.product.MoreImages);
                if (checkMoreImagesValue == false) { //check if MoreImages is not null or empty
                    //console.log('moreImg list: ', $scope.product.MoreImages)
                    //console.log('data img list: ', result.data.MoreImages)
                    $scope.moreImages = JSON.parse($scope.product.MoreImages)
                    //$scope.moreImages.push($scope.product.MoreImages)

                    //console.log('more images $scope ', $scope.moreImages)
                }

                CKEDITOR.instances['ckEditorContentEdit'].setData($scope.product.Content)
            }, function (error) {
                NotificationService.displayError(error.data);
            });
        }

        $scope.ChooseImages = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.product.Images = fileUrl;
                $scope.$apply();
            }
            finder.popup();
        }

        $scope.ChooseMoreImages = function () {
            var finder = new CKFinder();
            finder.selectActionData = "container";
            finder.selectActionFunction = function (fileUrl) {
                $scope.moreImages.push(fileUrl);
                $scope.$apply();
            }
            finder.popup();
        }

        loadParentCategory();
        loadProductDetail();
    }
})(angular.module('CoffeeShop.Products'));
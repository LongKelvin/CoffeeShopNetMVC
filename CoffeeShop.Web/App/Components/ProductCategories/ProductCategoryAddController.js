(function(app) {
    'use strict';

    app.controller('ProductCategoryAddController', ProductCategoryAddController);

    ProductCategoryAddController.$inject = ['$scope','ApiServices','$state', 'NotificationService'];

    function ProductCategoryAddController($scope,ApiServices,$state, NotificationService) {
        $scope.title = 'ProductCategoryAddController';

        $scope.productCategory =  {
            Status: true,
            CreatedDate: new Date(),
            HomeFlag: false,
            CreatedBy: 'AdminTest'
        }

        $scope.AddProductCategory = AddProductCategory;

        function AddProductCategory() {
            ApiServices.post('api/ProductCategory/Create', $scope.productCategory,
                function (result) {
                    NotificationService.displaySuccess(result.data.Name + ' đã được thêm mới.');
                    $state.go('ProductCategory');
                }, function (error) {
                    console.error(error)
                    NotificationService.displayError('Đã có lỗi xảy ra, Xin vui lòng thử lại.');
                });
        }
        function loadParentCategory() {
            ApiServices.get('api/ProductCategory/GetAllParents', null, function (result) {
                $scope.parentCategories = result.data;
            }, function () {
                console.log('Cannot get list parent');
            });
        }

        loadParentCategory();
    }
})(angular.module('CoffeeShop.ProductCategory'));
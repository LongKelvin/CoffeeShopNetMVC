(function (app) {
    app.controller('ProductCategoryEditController', ProductCategoryEditController);

    ProductCategoryEditController.$inject = ['$scope', 'ApiServices', '$state', 'NotificationService', '$stateParams'];

    function ProductCategoryEditController($scope, ApiServices, $state, NotificationService, $stateParams) {
        $scope.title = 'ProductCategoryEditController';

        $scope.productCategory = {
            UpdatedDate: new Date(),
            UpdatedBy: 'AdminTest'
        }

        $scope.UpdateProductCategory = UpdateProductCategory;

        function UpdateProductCategory() {
            $scope.productCategory.UpdatedDate = new Date();
            $scope.productCategory.UpdatedBy = 'AdminTest';

            ApiServices.post('api/ProductCategory/Update', $scope.productCategory,
                function (result) {
                    NotificationService.displaySuccess(result.data.Name + ' đã cập nhật thành công.');
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

        function loadProductCategoryDetail() {
            ApiServices.get('api/ProductCategory/GetById/' + $stateParams.id, null, function (result) {
                $scope.productCategory = result.data;
                

                console.log('productCategoryData: ',$scope.productCategory)
            }, function (error) {
                NotificationService.displayError(error.data);
            });
        }

        loadParentCategory();
        loadProductCategoryDetail();
       
    }
})(angular.module('CoffeeShop.ProductCategory'));
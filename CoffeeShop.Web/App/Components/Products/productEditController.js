(function (app) {
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

        function getSeoTitle() {
            $scope.product.Alias = CommonService.getSeoTitle($scope.product.Name);
        }

        function UpdateProduct() {
            $scope.product.UpdatedDate = new Date();
            $scope.product.UpdatedBy = 'AdminTest';

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
                console.log('Cannot get list parent');
            });
        }

        function loadProductDetail() {
            ApiServices.get('api/Product/GetById/' + $stateParams.id, null, function (result) {
                $scope.product = result.data;

                console.log('ProductData: ', $scope.product)
            }, function (error) {
                NotificationService.displayError(error.data);
            });
        }

        loadParentCategory();
        loadProductDetail();
    }
})(angular.module('CoffeeShop.Products'));
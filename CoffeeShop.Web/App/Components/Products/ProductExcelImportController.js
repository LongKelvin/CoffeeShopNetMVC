(function (app) {
    'use strict';

    app.controller('ProductExcelImportController', ProductExcelImportController);

    ProductExcelImportController.$inject = ['$scope', 'ApiServices', '$http', '$state', 'NotificationService', 'AuthenticationService', 'CommonService'];

    function ProductExcelImportController($scope, ApiServices, $http, $state, NotificationService, AuthenticationService, CommonService) {
        $scope.title = 'ProductExcelImportController';
        $scope.ImportProduct = ImportProduct;

        $scope.files = []
        $scope.categoryId = 0;

        function ImportProduct() {
            AuthenticationService.setHeader();

            //console.log('categoryId: ', $scope.categoryId)
            //console.log('files: ', $scope.files)

            $http({
                method: 'post',
                url: "api/Product/ImportFromExcel",
                headers: {
                    'Content-Type': undefined
                },
                transformRequest: function (data) {
                    var formData = new FormData();
                    formData.append("CategoryId", angular.toJson(data.categoryId));
                    for (let index = 0; index < data.files.length; index++) {
                        formData.append("file" + index, data.files[index]);
                    }

                    return formData;
                },
                data:
                {
                    categoryId: $scope.categoryId, files: $scope.files
                }
            }).then(
                function (result, status, headers, config) {
                    NotificationService.displaySuccess(result.data);
                    $state.go('Products')
                },
                function (result, status, headers, config) {
                    let message = result.data;
                    NotificationService.displayError(message);
                }
            );
        }
        function loadParentCategory() {
            ApiServices.get('api/ProductCategory/GetAllParents', null, function (result) {
                $scope.parentCategories = result.data;
            }, function () {
            });
        }

        $scope.SelectFile = function (e) {
            $.each(e.target.files, function (key, value) {
                $scope.files.push(value);
            });
        };

        loadParentCategory();
    }
})(angular.module('CoffeeShop.Products'));
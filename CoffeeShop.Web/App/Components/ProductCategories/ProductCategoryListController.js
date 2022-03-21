/// <reference path="../../shared/services/apiservices.js" />

(function (app) {
    app.controller('ProductCategoryListController', ProductCategoryListController);

    ProductCategoryListController.$inject = ['$scope', 'ApiServices'];

    function ProductCategoryListController($scope, ApiServices) {
        //setup Controller
        $scope.title = 'ProductCategoryListController';

        //Setup ApiServices

        $scope.productCategories = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        //asign function to get productCategory
        $scope.getProductCagories = getProductCagories;
        
        function getProductCagories(page) {
            page = page || 0;
            pageSize = $scope.pageSize || 10;
            var config = {
                params: {
                    page: page,
                    pageSize: pageSize
                }
            }
            try {
                ApiServices.get('/api/ProductCategory/GetAll', config, function (result) {
                    if (result.data.TotalCount == 0) {
                        notificationService.displayWarning('Không có dữ liệu để tải lên.');
                    }

                    $scope.productCategories = result.data.Items;
                    $scope.page = result.data.Page;
                    $scope.pagesCount = result.data.TotalPages; //total pages that the query recevied
                    $scope.totalCount = result.data.TotalCount; //total row data from api result
                    $scope.itemPerPage = pageSize;
                },
                    function () {
                        console.log('Load productcategory failed.');
                    });
            }
            catch (e) {
                console.log("Exception in getProductCategoies function: ")
                    (console.error || console.log).call(console, e.stack || e);
            }
        }

        $scope.pageChanged = function (newPage, pageSize) {
            if ($scope.page !== newPage) {
                getProductCagories($scope.page, pageSize)
            }
        };

        $scope.getProductCagories();

        
        }
    }) (angular.module('CoffeeShop.ProductCategory'));
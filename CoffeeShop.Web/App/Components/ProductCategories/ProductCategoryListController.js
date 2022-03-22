/// <reference path="../../shared/services/notificationservice.js" />
/// <reference path="../../shared/services/apiservices.js" />

(function (app) {
    app.controller('ProductCategoryListController', ProductCategoryListController);

    ProductCategoryListController.$inject = [
        '$scope',
        'ApiServices',
        'NotificationService',
        '$stateParams',
        '$state'];

    function ProductCategoryListController($scope, ApiServices, NotificationService/*, $stateParams, $state*/) {
        //setup Controller
        $scope.title = 'ProductCategoryListController';

        //Setup ApiServices

        $scope.productCategories = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.keyWord = '';
        //asign function to get productCategory
        $scope.getProductCagories = getProductCagories;

        function getProductCagories(page, pageSize) {
            page = page || 0;
            pageSize = pageSize || 20;

            var config = {
                params: {
                    keyWord: $scope.keyWord,
                    page: page,
                    pageSize: pageSize
                }
            }
            try {
                ApiServices.get('/api/ProductCategory/GetAll', config, function (result) {
                    if (result.data.TotalCount == 0) {
                        NotificationService.displayWarning('No data to display');
                    }

                    $scope.productCategories = result.data.Items;
                    $scope.page = result.data.Page;
                    $scope.pagesCount = result.data.TotalPages; //total pages that the query recevied
                    $scope.totalCount = result.data.TotalCount; //total row data from api result
                    $scope.itemPerPage = pageSize;
                },
                    function () {
                        console.log('Load productcategory failed.');
                        NotificationService.displayError('Load productcategory failed.');
                    });
            }
            catch (e) {
                console.log("Exception in getProductCategoies function: ")
                    (console.error || console.log).call(console, e.stack || e);

                NotificationService.displayError('Something went wrong, please try again later');
            }
        }

        //function goToProductCategoryAddView() {
        //    $state.go('ProductCategoryAdd')
        //}

        //excute when page loading done such as PageLoad
        $scope.getProductCagories();
    }
})(angular.module('CoffeeShop.ProductCategory'));
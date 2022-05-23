(function (app) {
    'use strict';

    app.controller('ApplicationUserListController', ApplicationUserListController);

    ApplicationUserListController.$inject = ['$scope', 'ApiService', 'NotificationService', '$ngBootbox'];

    function ApplicationUserListController($scope, ApiService, NotificationService, $ngBootbox) {
        $scope.loading = true;
        $scope.data = [];
        $scope.page = 0;
        $scope.pageCount = 0;
        $scope.search = search;
        $scope.clearSearch = clearSearch;
        $scope.deleteItem = deleteItem;

        function deleteItem(id) {
            $ngBootbox.confirm('Bạn có chắc muốn xóa?')
                .then(function () {
                    var config = {
                        params: {
                            id: id
                        }
                    }
                    ApiService.del('/api/ApplicationUser/Delete', config, function () {
                        NotificationService.displaySuccess('Đã xóa thành công.');
                        search();
                    },
                    function () {
                        NotificationService.displayError('Xóa không thành công.');
                    });
                });
        }
        function search(page) {
            page = page || 0;

            $scope.loading = true;
            var config = {
                params: {
                    page: page,
                    pageSize: 10,
                    filter: $scope.filterExpression
                }
            }

            ApiService.get('api/ApplicationUser/GetListPaging', config, dataLoadCompleted, dataLoadFailed);
        }

        function dataLoadCompleted(result) {
            $scope.data = result.data.Items;
            $scope.page = result.data.Page;
            $scope.pagesCount = result.data.TotalPages;
            $scope.totalCount = result.data.TotalCount;
            $scope.loading = false;

            if ($scope.filterExpression && $scope.filterExpression.length) {
                NotificationService.displayInfo(result.data.Items.length + ' items found');
            }
        }
        function dataLoadFailed(response) {
            NotificationService.displayError(response.data);
        }

        function clearSearch() {
            $scope.filterExpression = '';
            search();
        }

        $scope.search();
    }
})(angular.module('CoffeeShop.ApplicationUsers'));
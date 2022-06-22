/// <reference path="../../shared/services/notificationservice.js" />
/// <reference path="../../shared/services/apiservices.js" />

(function (app) {
    app.controller('ProductCategoryListController', ProductCategoryListController);

    ProductCategoryListController.$inject = [
        '$scope',
        'ApiServices',
        'NotificationService',
        '$stateParams',
        '$state',
        '$http'

    ];

    function ProductCategoryListController($scope, ApiServices, NotificationService) {
        //setup Controller
        $scope.title = 'ProductCategoryListController';

        //Setup ApiServices

        $scope.productCategories = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.keyWord = '';
        //asign function to get productCategory
        $scope.getProductCagories = getProductCagories;
        $scope.showDeleteDialog = showDeleteDialog;
        $scope.deleteProductCategory = deleleProductCategory;

        $scope.showMultiDeleteDialog = showMultiDeleteDialog;
        $scope.deleteMultiProductCategory = deleteMultiProductCategory;

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
                        //console.log('Load productcategory failed.');
                        NotificationService.displayError('Load productcategory failed.');
                    });
            }
            catch (e) {
                //console.log("Exception in getProductCategoies function: ")
                (console.error || console.log).call(console, e.stack || e);

                NotificationService.displayError('Something went wrong, please try again later');
            }
        }

        //function goToProductCategoryAddView() {
        //    $state.go('ProductCategoryAdd')
        //}

        //excute when page loading done such as PageLoad

        function showDeleteDialog(id) {
            $('#deleteId').val(id);
            $('#confirmDeleteModal').modal('show');
        }

        function deleleProductCategory() {
            var id = $('#deleteId').val();
            var config = {
                params: {
                    id: id
                }
            }
            ApiServices.del('api/ProductCategory/Delete', config, function () {
                NotificationService.displaySuccess('Xóa thành công');
                $('#confirmDeleteModal').modal('hide');
                getProductCagories();
            }, function () {
                $('#confirmDeleteModal').modal('hide');
                NotificationService.displayError('Xóa không thành công');
            })
        }

        function showMultiDeleteDialog() {
            var selectedItem = new Array();
            $('input:checkbox.checkBox').each(function () {
                if ($(this).prop('checked')) {
                    selectedItem.push($(this).val());
                }
            });

            //console.log('delete selected count: ', selectedItem);

            if (selectedItem.length <= 0) {
                //$('m-content').html("Vui lòng chọn ít nhất một bản ghi để xóa!");
                //$('#delMultiBtn').hide();
                //$('#confirmMultiDeleteModal').modal('show');
            }
            else {
                $('#totalDeleteCount').html(selectedItem.length);
                $('#confirmMultiDeleteModal').modal('show');
            }
        }

        function deleteMultiProductCategory() {
            var selectedIDs = [];
            $('input:checkbox.checkBox').each(function () {
                if ($(this).prop('checked')) {
                    selectedIDs.push($(this).val());
                }
            });

            //console.log('Console selectedIDs -> ', selectedIDs)

            var config = {
                params: {
                    ids: JSON.stringify(selectedIDs)
                }
            }

            //console.log('Param config: ', config)
            ApiServices.del('api/ProductCategory/DeleteMultiItems', config, function (result) {
                NotificationService.displaySuccess('Xóa thành công');
                $('#confirmDeleteModal').modal('hide');
                getProductCagories();
            }, function (error) {
                NotificationService.displayError('Xóa không thành công');
                $('#confirmDeleteModal').modal('hide');
            })
        }

        $scope.getProductCagories();
    }
})(angular.module('CoffeeShop.ProductCategory'));
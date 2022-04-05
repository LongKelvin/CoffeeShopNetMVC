
/// <reference path="../../shared/services/notificationservice.js" />
/// <reference path="../../shared/services/apiservices.js" />

(function (app) {
    app.controller('ProductListController', ProductListController);

    ProductListController.$inject = [
        '$scope',
        'ApiServices',
        'NotificationService',
        '$stateParams',
        '$state',
        '$http'

    ];

    function ProductListController($scope, ApiServices, NotificationService) {
        //setup Controller
        $scope.title = 'ProductListController';

        //Setup ApiServices

        $scope.productCategories = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.keyWord = '';
        //asign function to get Product
        $scope.getProducts = getProducts;
        $scope.showDeleteDialog = showDeleteDialog;
        $scope.deleteProduct = deleleProduct;

        $scope.showMultiDeleteDialog = showMultiDeleteDialog;
        $scope.deleteMultiProduct = deleteMultiProduct;

        function getProducts(page, pageSize) {
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
                ApiServices.get('/api/Product/GetAll', config, function (result) {
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
                        console.log('Load Product failed.');
                        NotificationService.displayError('Load Product failed.');
                    });
            }
            catch (e) {
                console.log("Exception in getProductCategoies function: ")
                    (console.error || console.log).call(console, e.stack || e);

                NotificationService.displayError('Something went wrong, please try again later');
            }
        }

        //function goToProductAddView() {
        //    $state.go('ProductAdd')
        //}

        //excute when page loading done such as PageLoad

        function showDeleteDialog(id) {
            $('#deleteId').val(id);
            $('#confirmDeleteModal').modal('show');
        }

        function deleleProduct() {
            var id = $('#deleteId').val();
            var config = {
                params: {
                    id: id
                }
            }
            ApiServices.del('api/Product/Delete', config, function () {
                NotificationService.displaySuccess('Xóa thành công');
                $('#confirmDeleteModal').modal('hide');
                getProducts();
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

            console.log('delete selected count: ', selectedItem);

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

        function deleteMultiProduct() {
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
            ApiServices.del('api/Product/DeleteMultiItems', config, function () {
                NotificationService.displaySuccess('Xóa thành công');
                $('#confirmDeleteModal').modal('hide');
                getProducts();
            }, function () {
                NotificationService.displayError('Xóa không thành công');
                $('#confirmDeleteModal').modal('hide');
            })
        }

        $scope.getProducts();
    }
})(angular.module('CoffeeShop.Product'));
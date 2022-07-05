/// <reference path="../../shared/services/notificationservice.js" />
/// <reference path="../../shared/services/apiservices.js" />
(function (app) {
    app.controller('OrderListController', OrderListController);

    OrderListController.$inject = [
        '$scope',
        'ApiServices',
        'NotificationService',
        'CommonService',
        '$stateParams',
        '$state',
        '$http'

    ];

    function OrderListController($scope, ApiServices, NotificationService, CommonService) {
        //setup Controller
        $scope.title = 'OrderListController';

        //Setup ApiServices

        $scope.orders = [];
        $scope.listOrderStatus = [];
        $scope.orderStatus = {};
        $scope.orderStatusBeforeChange = [];

        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.keyWord = '';
        //asign function to get Order
        $scope.getOrderList = getOrderList;
        $scope.showDeleteDialog = showDeleteDialog;
        $scope.showOrderUpdateStatusModal = showOrderUpdateStatusModal;
        $scope.deleteOrder = deleteOrder;

        $scope.showMultiDeleteDialog = showMultiDeleteDialog;
        $scope.deleteMultiOrder = deleteMultiOrder;

        $scope.getPaymentStatusStyle = getPaymentStatusStyle;
        $scope.getShippingStatusStyle = getShippingStatusStyle;
        $scope.getOrderStatusStyle = getOrderStatusStyle;
        $scope.loadListOrderStatus = loadListOrderStatus;

        $scope.setOrderStatusBeforeChange = setOrderStatusBeforeChange;
        $scope.updateOrderStatus = updateOrderStatus;
        $scope.showOrderUpdateConfirmStatusModal = showOrderUpdateConfirmStatusModal;
        $scope.confirmOrder = confirmOrder;
        $scope.cancelOrder = cancelOrder;
        $scope.showCancelOrderConfirm = showCancelOrderConfirm;

        function getOrderList(page, pageSize) {
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
                ApiServices.get('/api/Order/GetAll', config, function (result) {
                    if (result.data.TotalCount == 0) {
                        NotificationService.displayWarning('No data to display');
                    }

                    $scope.orders = result.data.Items;

                    $scope.page = result.data.Page;
                    $scope.pagesCount = result.data.TotalPages; //total pages that the query recevied
                    $scope.totalCount = result.data.TotalCount; //total row data from api result
                    $scope.itemPerPage = pageSize;
                },
                    function () {
                        //console.log('Load Order failed.');
                        NotificationService.displayError('Load Order failed.');
                    });
            }
            catch (e) {
                //console.log("Exception in getProductCategoies function: ")
                (console.error || console.log).call(console, e.stack || e);

                NotificationService.displayError('Something went wrong, please try again later');
            }
        }

        //function goToOrderAddView() {
        //    $state.go('OrderAdd')
        //}

        //excute when page loading done such as PageLoad

        function showDeleteDialog(id) {
            $('#deleteId').val(id);
            $('#confirmDeleteModal').modal('show');
        }

        function showOrderUpdateStatusModal(orderId) {
            $('#updateOrderStatusModal').modal('show');
            getOrderStatus(orderId);
        }

        function showOrderUpdateConfirmStatusModal(id) {
            $('#orderId').val(id);
            $('#confirmOrderModal').modal('show');
        }

        function showCancelOrderConfirm() {
            $('#updateOrderStatusModal').modal('hide');
            $('#cancelOrderModal').modal('show');
        }

        function confirmOrder() {
            var orderId = $('#orderId').val();
            ApiServices.post('api/Order/ConfirmOrder/' + orderId, null, function (response) {
                console.log(response)
                if (response.data == true) {
                    NotificationService.displaySuccess('Xác nhận đơn hàng thành công');
                    getOrderList();
                }
            }, function () {
                NotificationService.displayError('Xác nhận đơn hàng không thành công');
            });
            $('#confirmOrderModal').modal('hide');
        }

        //function deleleOrder() {
        //    var id = $('#deleteId').val();
        //    var config = {
        //        params: {
        //            id: id
        //        }
        //    }
        //    ApiServices.del('api/Order/Delete', config, function () {
        //        NotificationService.displaySuccess('Xóa thành công');
        //        $('#confirmDeleteModal').modal('hide');
        //        getOrderList();
        //    }, function () {
        //        $('#confirmDeleteModal').modal('hide');
        //        NotificationService.displayError('Xóa không thành công');
        //    })
        //}

        function loadListOrderStatus() {
            ApiServices.get('api/Order/GetListOrderStatus', null, function (response) {
                $scope.listOrderStatus = response.data;
            }, function () {
                NotificationService.displayError('Không thể load trạng thái đơn hàng');
            })
        }

        function cancelOrder() {
            if (CommonService.isNullOrEmpty($scope.orderStatus.Note)) {
                NotificationService.displayError('Bạn chưa nhập lý do hủy đơn hàng!');
                return;
            }

            ApiServices.post('api/Order/CancelOrder', $scope.orderStatus, function (response) {
                console.log(response)
                if (response.data == true) {
                    NotificationService.displaySuccess('Hủy đơn hàng thành công');
                    getOrderList();
                }
            }, function () {
                NotificationService.displayError('Hủy đơn hàng không thành công');
            })
            $('#cancelOrderModal').modal('hide');
        }

        function updateOrderStatus() {
            ApiServices.post('api/Order/UpdateOrderStatus', $scope.orderStatus, function (response) {
                //console.log(response)
                if (response.data == true) {
                    NotificationService.displaySuccess('Cập nhật đơn hàng thành công');
                    getOrderList();
                }
            }, function () {
                NotificationService.displayError('Cập nhật trạng thái không thành công');
            })
        }

        function deleteOrder() {
            var id = $('#deleteId').val();
            ApiServices.del('api/Order/DeleteOrder/' + id, null, function (response) {
                //console.log(response)
                if (response.data == true) {
                    NotificationService.displaySuccess('Đơn hàng đã được xóa thành công');
                    getOrderList();
                }
            }, function () {
                NotificationService.displayError('Xóa đơn hàng thất bại');
            })

            $('#confirmDeleteModal').modal('hide');
        }

        function getOrderStatus(id) {
            ApiServices.get('api/Order/GetOrderStatus/' + id, null, function (response) {
                $scope.orderStatus = response.data;
                $scope.orderStatusBeforeChange = response.data;
            }, function () {
                NotificationService.displayError('Không thể load trạng thái đơn hàng');
            })
        }

        function setOrderStatusBeforeChange() {
            $scope.orderStatus = $scope.orderStatusBeforeChange;
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

        function deleteMultiOrder() {
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
            ApiServices.del('api/Order/DeleteMultiIOrder', config, function (result) {
                NotificationService.displaySuccess('Xóa thành công');
                $('#confirmDeleteModal').modal('hide');
                getOrderList();
            }, function (error) {
                NotificationService.displayError('Xóa không thành công');
                $('#confirmDeleteModal').modal('hide');
            })

            $('#confirmMultiDeleteModal').modal('hide');
        }

        function getPaymentStatusStyle(input) {
            switch (input) {
                case 0: {
                    return 'badge bg-warning'
                }

                case 1: {
                    return 'badge bg-success'
                }

                case 2: {
                    return "badge badge-dark"
                }
                case 3: {
                    return "badge badge-danger"
                }
            }
        }

        function getOrderStatusStyle(input) {
            switch (input) {
                case 0: {
                    return 'badge bg-warning'
                }

                case 1: {
                    return 'badge bg-info'
                }

                case 2: {
                    return "badge badge-info"
                }
                case 3: {
                    return "badge badge-primary"
                }
                case 4: {
                    return "badge badge-success"
                }
                case 5: {
                    return "badge badge-danger"
                }
                case 6: {
                    return "badge badge-secondary"
                }
                case 7: {
                    return "badge badge-danger"
                }
            }
        }

        function getShippingStatusStyle(input) {
            switch (input) {
                case 0: {
                    return 'badge bg-warning'
                }

                case 1: {
                    return 'badge bg-primary'
                }

                case 2: {
                    return "badge badge-success"
                }
                case 3: {
                    return "badge badge-secondary"
                }
                case 4: {
                    return "badge badge-danger"
                }
            }
        }

        $scope.getOrderList();
        $scope.loadListOrderStatus();
    }
})(angular.module('CoffeeShop.Orders'));
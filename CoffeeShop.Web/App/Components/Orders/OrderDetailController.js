(function (app) {
    app.controller('OrderDetailController', OrderDetailController);

    OrderDetailController.$inject = [
        '$scope',
        'ApiServices',
        '$state',
        'NotificationService',
        '$stateParams',
        'CommonService'

    ];

    function OrderDetailController($scope, ApiServices, $state, NotificationService, $stateParams, CommonService) {
        //setup Controller
        $scope.title = 'OrderDetailController';

        //Setup ApiServices

        $scope.orderDetail = {}
        $scope.listOrderStatus = [];
        $scope.listPaymentStatus = [];
        $scope.listShippingStatus = [];

        $scope.orderStatus = {};
        $scope.orderStatusBeforeChange = {};

        $scope.getPaymentStatusStyle = getPaymentStatusStyle;
        $scope.getShippingStatusStyle = getShippingStatusStyle;
        $scope.getOrderStatusStyle = getOrderStatusStyle;
        $scope.loadListOrderStatus = loadListOrderStatus;
        $scope.loadListPaymentStatus = loadListPaymentStatus;
        $scope.loadListShippingStatus = loadListShippingStatus;

        $scope.setOrderStatusBeforeChange = setOrderStatusBeforeChange;
        $scope.updateOrderStatus = updateOrderStatus;

        $scope.cancelOrder = cancelOrder;
        $scope.getOrderStatus = getOrderStatus;
        $scope.loadOrderDetail = loadOrderDetail;

        function loadOrderDetail() {
            ApiServices.get('api/Order/GetOrderDetail/' + $stateParams.id, null, function (response) {
                console.log('detail ', response)
                $scope.orderDetail = response.data;
                $scope.orderDetail.CreatedDate = new Date($scope.orderDetail.CreatedDate);
                //Format currency 
                $scope.orderDetail.TotalAmountF = $scope.orderDetail.TotalAmount.toLocaleString('it-IT', { style: 'currency', currency: 'VND' });
                $scope.orderDetail.TotalItemPriceF = $scope.orderDetail.TotalItemPrice.toLocaleString('it-IT', { style: 'currency', currency: 'VND' });
                $scope.orderDetail.ShippingFeeF = $scope.orderDetail.ShippingFee.toLocaleString('it-IT', { style: 'currency', currency: 'VND' });

                console.log('order detail: ', $scope.orderDetail)
            }, function (error) {
                NotificationService.displayError(error.data);
            });
        }

        function loadListOrderStatus() {
            ApiServices.get('api/Order/GetListOrderStatus', null, function (response) {
                $scope.listOrderStatus = response.data;
            }, function () {
                NotificationService.displayError('Không thể load trạng thái đơn hàng');
            })
        }

        function loadListPaymentStatus() {
            ApiServices.get('api/Order/GetListPaymentStatus', null, function (response) {
                $scope.listPaymentStatus = response.data;
            }, function () {
                NotificationService.displayError('Không thể load trạng thái thanh toán');
            })
        }

        function loadListShippingStatus() {
            ApiServices.get('api/Order/GetListShippingStatus', null, function (response) {
                $scope.listShippingStatus = response.data;
            }, function () {
                NotificationService.displayError('Không thể load trạng thái vận chuyển ');
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
            $state.go("Orders")
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

        $scope.loadListOrderStatus();
        $scope.loadListPaymentStatus();
        $scope.loadListShippingStatus();
        $scope.loadOrderDetail();
    }
})(angular.module('CoffeeShop.Orders'));
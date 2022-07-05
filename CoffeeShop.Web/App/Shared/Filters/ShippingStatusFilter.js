(function (app) {
    app.filter('shippingStatusFilter', function () {
        return function (input) {
            switch (input) {
                case 0: {
                    return "Chưa giao hàng"
                }

                case 1: {
                    return "Đang giao hàng"
                }

                case 2: {
                    return "Đã nhận hàng"
                }
                case 3: {
                    return "Không yêu cầu giao hàng"
                    
                }
                case 4: {
                    return "Đã hủy"
                }
            }
        }
    });
})(angular.module('CoffeeShop.Common'))
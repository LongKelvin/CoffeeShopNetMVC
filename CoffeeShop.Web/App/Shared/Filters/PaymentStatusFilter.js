(function (app) {
    app.filter('paymentStatusFilter', function () {
        return function (input) {
            switch (input) {
                case 0: {
                    input = "Chưa thanh toán";
                    input.class = 'badge bg-success';
                    return input;
                }

                case 1: {
                    return "Đã thanh toán"
                }

                case 2: {
                    return "Đã trả hàng"
                }
                case 3: {
                    return "Đã hủy"
                }
            }
        }
    });
})(angular.module('CoffeeShop.Common'))
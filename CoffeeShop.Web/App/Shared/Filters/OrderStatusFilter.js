(function (app) {
    app.filter('orderStatusFilter', function () {
        return function (input) {
            switch (input) {
                case 0: {
                    return "Chưa xử lý"
                }

                case 1: {
                    return "Đã xác nhận"
                }

                case 2: {
                    return "Đang chuẩn bị hàng - đóng gói"
                }
                case 3: {
                    return "Đang giao hàng"
                }
                case 4: {
                    return "Hoàn thành"
                }
                case 5: {
                    return "Đã bị hủy"
                }
                case 6: {
                    return "Đã bị trả hàng"
                }
                case 7: {
                    return "Thất bại"
                }
            }
        }
    });
})(angular.module('CoffeeShop.Common'))
(function (app) {
    'use strict';

    app.controller('RevenueStatisticController', RevenueStatisticController);

    RevenueStatisticController.$inject = ['$scope', 'ApiServices', 'NotificationService'];

    function RevenueStatisticController($scope, ApiServices, NotificationService) {
        $scope.GetRevenueStatistic = GetRevenueStatistic;
        $scope.data = [];
        //$scope.chartDataRevenues = []
        //$scope.chartDataBenefit = [];
        //$scope.chartLabels = [];

        function GetRevenueStatistic() {
            var fromDate = $('#txtFromDate').val();
            var toDate = $('#txtToDate').val();

            if (util.isNullOrEmptyString(fromDate)) {
                //get the first day of current month
                fromDate = util.getFirstDateOfCurrentMonth().toString();
            }

            if (util.isNullOrEmptyString(toDate)) {
                //get the first day of current month
                toDate = util.getCurrentDayWithoutTime().toString();
            }

            fromDate = util.removeTimeFromDate(fromDate);
            toDate = util.removeTimeFromDate(toDate);

            var config = {
                param: {
                    fromDate: fromDate,
                    toDate: toDate
                }
            }

            try {
                ApiServices.get('/api/Statistic/GetRevenue?fromDate=' + config.param.fromDate + '&toDate=' + config.param.toDate, null, function (result) {
                    if (result.data == null) {
                        NotificationService.displayWarning('No data to display');
                    }

                    $scope.data = result.data;

                    //var labels = [];
                    //var revenues = [];
                    //var benefits = [];

                    //$.each(result.data, function (i, item) {
                    //    labels.push(item.Date);
                    //    revenues.push(item.Revenues);
                    //    benefits.push(item.Benefit);

                    //});

                    //$scope.chartLabels.push(labels);
                    //$scope.chartDataRevenues.push(revenues);
                    //$scope.chartDataBenefit.push(benefits);
                },
                    function () {
                        NotificationService.displayError('Load Product failed.');
                    });
            }
            catch (e) {
                NotificationService.displayError('Something went wrong, please try again later');
            }
        }

        GetRevenueStatistic();
    }
})(angular.module('CoffeeShop.Statistics'));
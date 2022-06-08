(function () {
    angular.module('CoffeeShop.Statistics', ['CoffeeShop.Common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('StatisticRevenues', {
            url: "/Statistic/Revenue",
            templateUrl: "/App/Components/Statistics/RevenueStatisticView.html",
            parent: 'Base',
            controller: "RevenueStatisticController"
        })
        //.state('ApplicationUserAdd', {
        //    url: "/ApplicationUser/Add",
        //    parent: 'Base',
        //    templateUrl: "/App/Components/ApplicationUsers/ApplicationUserAddView.html",
        //    controller: "ApplicationUserAddController"
        //})
        //.state('ApplicationUserEdit', {
        //    url: "/ApplicationUser/Edit/:id",
        //    templateUrl: "/App/Components/ApplicationUsers/ApplicationUserEditView.html",
        //    controller: "ApplicationUserEditController",
        //    parent: 'Base',
        //});
    }
})();
(function () {
    angular.module('CoffeeShop.Orders', [

        // Custom modules
        'CoffeeShop.Common',

    ]).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('Orders', {
                url: "/Orders",
                parent: 'Base',
                templateUrl: "/App/Components/Orders/OrderListView.html",
                controller: "OrderListController"
            })
            .state('OrderDetail', {
                url: "/Order/Detail/:id",
                parent: 'Base',
                templateUrl: "/App/Components/Orders/OrderDetailView.html",
                controller: "OrderDetailController"
            })
            .state('OrderEdit', {
                url: "/Order/Edit/:id",
                parent: 'Base',
                templateUrl: "/App/Components/Orders/OrderEditView.html",
                controller: "OrderEditController"
            });
    }
})();

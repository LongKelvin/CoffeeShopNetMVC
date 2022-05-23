

(function () {
    angular.module('CoffeeShop.ApplicationUsers', ['CoffeeShop.Common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {

        $stateProvider.state('ApplicationUsers', {
            url: "/ApplicationUsers",
            templateUrl: "/App/Components/ApplicationUsers/ApplicationUserListView.html",
            parent: 'Base',
            controller: "ApplicationUserListController"
        })
            .state('ApplicationUserAdd', {
                url: "/ApplicationUser/Add",
                parent: 'Base',
                templateUrl: "/App/Components/ApplicationUsers/ApplicationUserAddView.html",
                controller: "ApplicationUserAddController"
            })
            .state('ApplicationUserEdit', {
                url: "/ApplicationUser/Edit/:id",
                templateUrl: "/App/Components/ApplicationUsers/ApplicationUserEditView.html",
                controller: "ApplicationUserEditController",
                parent: 'Base',
            });
    }
})();
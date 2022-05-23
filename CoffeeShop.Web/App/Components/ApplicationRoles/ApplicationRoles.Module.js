(function () {
    angular.module('CoffeeShop.ApplicationRoles', [

        // Custom modules
        'CoffeeShop.Common',

    ]).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('ApplicationRoles', {
                url: "/ApplicationRoles",
                parent: 'Base',
                templateUrl: "/App/Components/ApplicationRoles/ApplicationRoleListView.html",
                controller: "ApplicationRoleListController"
            })
            .state('ApplicationRoleAdd', {
                url: "/ApplicationRole/Add",
                parent: 'Base',
                templateUrl: "/App/Components/ApplicationRoles/ApplicationRoleAddView.html",
                controller: "ApplicationRoleAddController"
            })
            .state('ApplicationRoleEdit', {
                url: "/ApplicationRole/Edit/:id",
                parent: 'Base',
                templateUrl: "/App/Components/ApplicationRoles/ApplicationRoleEditView.html",
                controller: "ApplicationRoleEditController"
            });
    }
})();
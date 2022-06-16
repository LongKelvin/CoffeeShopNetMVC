/// <reference path="../../../assets/admin/lib/angular.js/angular.js" />

(function () {
    angular.module('CoffeeShop.ApplicationGroups', [

        // Custom modules
        'CoffeeShop.Common',

    ]).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('ApplicationGroups', {
                url: "/ApplicationGroup",
                parent: 'Base',
                templateUrl: "/App/Components/ApplicationGroups/ApplicationGroupListView.html",
                controller: "ApplicationGroupListController"
            })
            .state('ApplicationGroupAdd', {
                url: "/ApplicationGroup/Add",
                parent: 'Base',
                templateUrl: "/App/Components/ApplicationGroups/ApplicationGroupAddView.html",
                controller: "ApplicationGroupAddController"
            })
            .state('ApplicationGroupEdit', {
                url: "/ApplicationGroup/Edit/:id",
                parent: 'Base',
                templateUrl: "/App/Components/ApplicationGroups/ApplicationGroupEditView.html",
                controller: "ApplicationGroupEditController"
            });
    }
})();
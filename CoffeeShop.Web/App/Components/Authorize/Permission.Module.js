(function () {
    angular.module('CoffeeShop.AppPermissions', ['CoffeeShop.Common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('AccessDenied', {
            url: "/ApplicationPermission/AccessDenied",
            templateUrl: "/App/Components/Authorize/PermissionDenied.html",
            parent: 'Base',
            controller: "PermissionDeniedController"
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
/// <reference path="../../../assets/admin/lib/angular.js/angular.js" />

(function () {
    angular.module('CoffeeShop.Slides', [

        // Custom modules
        'CoffeeShop.Common'

    ]).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('Slides', {
                url: "/Slides",
                parent: 'Base',
                templateUrl: "/App/Components/Slides/SlideListView.html",
                controller: "SlideListController"
            })
            .state('SlideAdd', {
                url: "/Slides/Add",
                parent: 'Base',
                templateUrl: "/App/Components/Slides/SlideAddView.html",
                controller: "SlideAddController"
            })
            .state('SlideEdit', {
                url: "/Slides/Edit/:id",
                parent: 'Base',
                templateUrl: "/App/Components/Slides/SlideEditView.html",
                controller: "SlideEditController"
            });
    }
})();
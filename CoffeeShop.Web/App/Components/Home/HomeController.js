(function (app) {
    app.controller('HomeController', HomeController);

    HomeController.$inject = ['$scope'];

    function HomeController($scope) {
        $scope.title = 'HomeController';

        activate();

        function activate() { }
    }
})(angular.module('CoffeeShop'));
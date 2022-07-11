
////(function (app) {
////    app.controller('RootController', RootController);
////    RootController.$inject = ['$scope', '$state', 'AuthData', 'LoginService', 'AuthenticationSerice']

////    function RootController($scope, $state, AuthData, LoginService, AuthenticationService) {
////        $scope.logOut = function () {
////            //LoginService.logOut();
////            $state.go('Login')
////        }
////        //$scope.authentication = AuthData.authenticationData;

////        //AuthenticationService.validateRequest();
////    }
////})(angular.module('CoffeeShop'))

(function (app) {
    app.controller('RootController', RootController);
    RootController.$inject = ['$scope', '$state', '$window', 'AuthData', 'LoginService', 'AuthenticationService', 'ApiServices', 'NotificationService']

    function RootController($scope, $state, $window, AuthData, LoginService, AuthenticationService, ApiServices, NotificationService) {
        $scope.logOut = function () {
            LoginService.logOut();
            //console.log(AuthData.authenticationData)
            delete $window.sessionStorage["TokenInfo"]
            $state.go('Login')
        }

        $scope.authentication = AuthData.authenticationData;
        $scope.getAppNotification = getAppNotification;
        $scope.getNotificationStyle = getNotificationStyle;
        $scope.getNotificationFromServer = getNotificationFromServer;
        $scope.updateNotificationReadedStatus = updateNotificationReadedStatus;

        function getAppNotification() {
            try {
                ApiServices.get('/api/Notification/GetTop10Notification', null, function (result) {
                    //console.log('result = ', result);
                    $scope.ListNotification = result.data.ListNotification;
                    $scope.TotalNewNotification = result.data.TotalNewNotification;
                },
                    function () {
                        //console.log('Load Product failed.');
                        NotificationService.displayError('Load Product failed.');
                    });
            }
            catch (e) {
                //console.log("Exception in getProductCategoies function: ")
                //    (console.error || console.log).call(console, e.stack || e);

                NotificationService.displayError('Something went wrong, please try again later');
            }
        }

        function getNotificationStyle(status) {
            switch (status) {
                case false: {
                    return 'new-notification'
                }

                case true: {
                    return ''
                }
            }
        }

        //Hub connection
        function getNotificationFromServer() {
            // Reference the auto-generated proxy for the hub.
            var notificationHub = $.connection.notificationHub;
            //console.log('notificationHub connection: ', notificationHub);
            //// Create a function that the hub can call back to display messages.
            notificationHub.client.addNewMessageToPage = function (message) {

                getAppNotification();
                NotificationService.displaySuccess('Bạn có 1 đơn hàng mới,  Mã đơn #' + message);
                playAudio();
            };

            // Start the connection.
            $.connection.hub.logging = true;
            $.connection.hub.start().done(function () {

                //    Call the Send method on the hub.
                //    notificationHub.server.send($('#displayname').val(), $('#message').val());

                //console.log('$.connection.hub.start().done')
            });
        }

        function playAudio() {
            var audio = new Audio('/Assets/Admin/audio/sweet_text.mp3');
            audio.loop = false;
            //console.log(audio);
            audio.play();
        };

        function updateNotificationReadedStatus(id) {
            ApiServices.post('/api/Notification/UpdateReadedStatus/' + id, null, function (result) {
                getAppNotification();
            },
                function () {
                    console.log('update notification failed.');

                });
        }

        $scope.getNotificationFromServer();
        $scope.getAppNotification();

    }
})(angular.module('CoffeeShop'))
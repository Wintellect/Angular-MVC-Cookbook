
angular
    .module('myApp.ctrl.home', [])
    .controller('homeCtrl', [
        '$scope',
        '$location',
        function ($scope, $location) {

            $scope.navigationManager = navigationManagerFactory();

            function navigationManagerFactory() {
                var listPath = '/';
                return {
                    setListPage: function () {
                        listPath = $location.path();
                    },
                    goToListPage: function () {
                        $location.path(listPath);
                    }
                };
            }

        }]);
